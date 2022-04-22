using Api.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace Api
{
    public class alicakil
    {
        public void CreateDatabase()
        {
            Context c = new Context();

            //Create a database with Dummy data(if not there)... 
            if ((c.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                c.Database.EnsureDeleted();
            }

            if (!(c.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                c.Database.EnsureCreated();

                // Create Master Data..
                c.InvoicesStatus.Add(new InvoiceStatus() { Name = "draft" });
                c.InvoicesStatus.Add(new InvoiceStatus() { Name = "issued" });
                c.InvoicesStatus.Add(new InvoiceStatus() { Name = "cancelled" });
                c.SaveChanges();

                c.CustomerTypes.Add(new CustomerType() { Name= "employee of the store" });
                c.CustomerTypes.Add(new CustomerType() { Name= "affiliate of the store" });
                c.SaveChanges();

                c.Customers.Add(new Customer() { Name="Customer1", Password = "pass123123", TypeId = 1 });  // Employee of the main store
                c.Customers.Add(new Customer() { Name="Customer2", Password = "pass123123", TypeId = 2 });  // Employee of an other store
                c.Customers.Add(new Customer() { Name="Customer3", Password = "pass123123" });  // Not categorized
                c.SaveChanges();

                c.Invoices.Add(new Invoice() { Ammount=1000, Discounted=1000, Customerid=3, Statusid= InvoiceStatus.draft }); // sample invoce record
                c.Invoices.Add(new Invoice() { Ammount=100, Discounted=200, Customerid=3, Statusid= InvoiceStatus.draft });   // sample invoce record
                c.SaveChanges();

            }
            
        }

    }
}
