using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repo;
using Api.ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        ICustomerRepo _Customers;
        ILogger<DiscountController> _logger;

        public DiscountController(ICustomerRepo Customers, ILogger<DiscountController> logger)
        {
            _Customers = Customers;
            _logger = logger;
        }


        [HttpGet("CreateBill")]
        public IActionResult CreateBill(int Customerid, double billAmmount)
        {
            try
            {
                _logger.LogInformation(LogCategory.Start, "CreateBill triggered, Customerid:" + Customerid + ", billAmmount:" + billAmmount);

                Context c = new Context();
                Customer customer = _Customers.GetById(Customerid);

                if (customer == null)
                    return NotFound("Customer not found");

                Invoice invoice = new Invoice();
                invoice.Customerid = Customerid;
                invoice.Ammount = billAmmount;
                invoice.Discounted = GetDiscount(customer, billAmmount); // Calculate the discount
                invoice.Statusid = InvoiceStatus.draft;

                c.Invoices.Add(invoice);
                c.SaveChanges();

                return Ok(InvoiceVM.GetViewModel(invoice, "Bill created succesfully!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogCategory.ExceptionError, "CreateBill, error:" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }


        

        [HttpGet("UpdateBillAmount")]
        public IActionResult UpdateBillAmount(int invoiceid, double billAmmount)
        {
            Context c = new Context();

            Invoice invoice = c.Invoices.FirstOrDefault(x => x.Id == invoiceid);
            Customer customer = c.Customers.FirstOrDefault(x => x.id== invoice.Customerid);

            if (invoice == null)
            {
                _logger.LogWarning(LogCategory.NullWarning, "UpdateBillAmount, invoice not found, invoiceid:" + invoiceid);
                return NotFound("invoice not found");
            }
               

            if (customer == null)
            {
                _logger.LogWarning(LogCategory.NullWarning, "UpdateBillAmount, customer not found, invoiceid:" + invoice.Customerid);
                return NotFound("customer not found");
            }
                

            if (invoice.Statusid != InvoiceStatus.draft)
            {
                _logger.LogWarning(LogCategory.InvalidInputWarning, "UpdateBillAmount, You can update an invoice if it is in draft status only!");
                return UnprocessableEntity("You can update an invoice if it is in draft status only!");
            }

            invoice.Ammount = billAmmount;
            invoice.Discounted = GetDiscount(customer, billAmmount);

            c.Invoices.Update(invoice);
            c.SaveChanges();

            _logger.LogInformation("Bill updated succesfully! invid:" + invoice.Id);
            return Ok(InvoiceVM.GetViewModel(invoice, "Bill updated succesfully!"));
        }

        [HttpGet("IssueBill")]
        public IActionResult IssueBill(int Invoiceid)
        {
            Context c = new Context();

            Invoice invoice = c.Invoices.FirstOrDefault(x => x.Id == Invoiceid);

            if (invoice == null)
                return NotFound("Customer not found");

            if(invoice.Statusid == InvoiceStatus.issued)
                return UnprocessableEntity("This invoice is already issued!");

            if (invoice.Statusid == InvoiceStatus.cancelled)
                return UnprocessableEntity("This invoice is cancelled. Please a create new one!");

            invoice.Statusid = InvoiceStatus.issued;
            c.Invoices.Update(invoice);
            c.SaveChanges();

            return Ok(InvoiceVM.GetViewModel(invoice, "Bill issued succesfully!"));
        }


        [HttpGet("CancelBill")]
        public IActionResult CancelBill(int Invoiceid)
        {
            Context c = new Context();

            Invoice invoice = c.Invoices.FirstOrDefault(x => x.Id == Invoiceid);

            if (invoice == null)
                return NotFound("Customer not found");

            if (invoice.Statusid != InvoiceStatus.issued)
                return UnprocessableEntity("The invoice is not issued to be canceled!");

            invoice.Statusid = InvoiceStatus.cancelled;
            c.Invoices.Update(invoice);
            c.SaveChanges();

            return Ok(InvoiceVM.GetViewModel(invoice, "Bill cancelled succesfully!"));
        }



        interface IDiscount
        {
            double GetDiscount(double billAmmount);
        }


        class AnEmployeeDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.7;  // 30% discount
            }
        }


        class AnAffiateDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.9;  // 10% discount
            }
        }

        class OldCustomerDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.95;  // 5% discount
            }
        }

        [NonAction]
        double GetDiscount(Customer customer, double billAmmount)
        {
            double discounted = 0;

            IDiscount Idiscount;

            if (customer.TypeId == CustomerType.AnEmployee)
            {
                Idiscount = new AnEmployeeDiscount();
                discounted = Idiscount.GetDiscount(billAmmount);
            }
            else if (customer.TypeId == CustomerType.AnAffiate)
            {
                Idiscount = new AnAffiateDiscount();
                discounted = Idiscount.GetDiscount(billAmmount);
            }
            else
            {
                if (customer.CreateTime < DateTime.Now.AddYears(-2)) // this is an old customer : 5% discount
                {
                    Idiscount = new OldCustomerDiscount();
                    discounted = Idiscount.GetDiscount(billAmmount);
                }
                else
                    discounted = billAmmount;  //  no discount
            }

            double AddditionalDiscount = Math.Round(billAmmount / 100) * 5; // 5$ discount for each 100$ on the bill.
            discounted -= AddditionalDiscount;

            return discounted;
        }

   

    }
}


