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

        public DiscountController(ICustomerRepo Customers)
        {
            _Customers = Customers;
        }


        [HttpGet("CreateBill")]
        public IActionResult CreateBill(int Customerid, double billAmmount)
        {
            Context c = new Context();
            Customer customer = c.Customers.FirstOrDefault(x => x.id == Customerid);

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


        

        

        [HttpGet("UpdateBillAmount")]
        public IActionResult UpdateBillAmount(int invoiceid, double billAmmount)
        {
            Context c = new Context();

            Invoice invoice = c.Invoices.FirstOrDefault(x => x.Id == invoiceid);
            Customer customer = c.Customers.FirstOrDefault(x => x.id== invoice.Customerid);

            if (invoice == null)
                return NotFound("invoice not found");

            if (customer == null)
                return NotFound("customer not found");

            if (invoice.Statusid != InvoiceStatus.draft)
            {
                return UnprocessableEntity("You can update an invoice if it is in draft status only!");
            }

            invoice.Ammount = billAmmount;
            invoice.Discounted = GetDiscount(customer, billAmmount);

            c.Invoices.Update(invoice);
            c.SaveChanges();

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



        [NonAction]
        double GetDiscount(Customer customer, double billAmmount)
        {
            double discounted = 0;

            if (customer.TypeId == CustomerType.AnEmployee)
                discounted =  billAmmount * 0.7;  // Employee of the main store

            else if (customer.TypeId == CustomerType.AnAffiate)
                discounted =  billAmmount * 0.9;  // Employee of an affiliate store
            else
            {
                if (customer.CreateTime < DateTime.Now.AddYears(-2)) // this is an old customer : 5% discount
                    discounted = billAmmount * 0.95;
                else
                    discounted = billAmmount;  //  no discount
            }

            double AddditionalDiscount = Math.Round(billAmmount / 100) * 5; // 5$ discount for each 100$ on the bill.
            discounted -= AddditionalDiscount;

            return discounted;
        }

   

    }
}


