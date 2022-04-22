using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repo;
using Api.ViewModels;
using Api.BusinessLogic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Api.Controllers
{
    


    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IHostingEnvironment _env;

        ICustomerRepo _Customers;
        IInvoiceRepo _Invoices;
        ILogger<DiscountController> _logger;

       

        public DiscountController(ICustomerRepo Customers, IInvoiceRepo invoiceRepo, ILogger<DiscountController> logger)
        {
            _Customers = Customers;
            _Invoices = invoiceRepo;
            _logger = logger;
        }


        [HttpPost("CreateBill")]
        public IActionResult CreateBill(int Customerid, double billAmmount)
        {
            try
            {
                _logger.LogInformation(LogCategory.Start, "CreateBill triggered, Customerid:" + Customerid + ", billAmmount:" + billAmmount);

                Context c = new Context();
                Customer customer =  _Customers.GetById(Customerid);

                if (customer == null)
                    return NotFound("Customer not found");


                Invoice invoice = new Invoice();
                invoice.Customerid = Customerid;
                invoice.Ammount = billAmmount;
                invoice.Discounted = DiscountLogic.GetDiscount(customer, billAmmount); // Calculate the discount
                invoice.Statusid = InvoiceStatus.draft;

                _Invoices.Create(invoice);

                return Ok(InvoiceVM.GetViewModel(invoice, "Bill created succesfully!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogCategory.ExceptionError, "CreateBill, error:" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }


        [HttpPut("UpdateBillAmount")]
        public IActionResult UpdateBillAmount(int invoiceid, double billAmmount)
        {
            Invoice invoice = _Invoices.GetById(invoiceid);
            Customer customer = _Customers.GetById(invoice.Customerid);

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
            invoice.Discounted = DiscountLogic.GetDiscount(customer, billAmmount);

            _Invoices.Update(invoice);

            _logger.LogInformation("Bill updated succesfully! invid:" + invoice.Id);
            return Ok(InvoiceVM.GetViewModel(invoice, "Bill updated succesfully!"));
        }

        [HttpPut("IssueBill")]
        public IActionResult IssueBill(int Invoiceid)
        {
            Invoice invoice = _Invoices.GetById(Invoiceid);

            if (invoice == null)
                return NotFound("Invoice not found");

            if(invoice.Statusid == InvoiceStatus.issued)
                return BadRequest("This invoice is already issued!");

            if (invoice.Statusid == InvoiceStatus.cancelled)
                return BadRequest("This invoice is cancelled. Please a create new one!");

            invoice.Statusid = InvoiceStatus.issued;

            _Invoices.Update(invoice);

            return Ok(InvoiceVM.GetViewModel(invoice, "Bill issued succesfully!"));
        }


        [HttpPut("CancelBill")]
        public IActionResult CancelBill(int Invoiceid)
        {
            Invoice invoice = _Invoices.GetById(Invoiceid);

            if (invoice == null)
                return NotFound("Customer not found");

            if (invoice.Statusid != InvoiceStatus.issued)
                return UnprocessableEntity("The invoice is not issued to be canceled!");

            invoice.Statusid = InvoiceStatus.cancelled;

            _Invoices.Update(invoice);

            return Ok(InvoiceVM.GetViewModel(invoice, "Bill cancelled succesfully!"));
        }


        [HttpPut("GetBill")]
        public IActionResult GetInvoice(int Invoiceid)
        {
            Invoice invoice = _Invoices.GetById(Invoiceid);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(InvoiceVM.GetViewModel(invoice, "invoice information"));
        }

        [HttpGet("ExportInvoices")]
        public IActionResult ExportInvoices()
        {
            List<Invoice> invoices = _Invoices.GetAll();
            

            if (invoices == null)
            {
                return NotFound();
            }
            

            var exportdata = invoices.Select(x=> new { x.Id, x.Customerid, x.Ammount, x.Discounted, x.CreateTime }).ToList();

            Api.Library.Excel e = new Library.Excel();

            ActionResult file = e.DownloadExcel("invoices", exportdata);
            if (file == null)
            {
                return NotFound("file not found!");
            }

            return file;
        }







    }
}


