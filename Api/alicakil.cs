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

                c.Stores.Add(new Store() { Name = "Main Store (ShopsRUs)", isMainStore = true });
                c.Stores.Add(new Store() { Name = "Some Other Store 1", isMainStore = false });
                c.Stores.Add(new Store() { Name = "Some Other Store 2", isMainStore = false });

                c.Users.Add(new User() { Name="user1", Storeid = 1  });  // Employee of the main store
                c.Users.Add(new User() { Name="user2", Storeid = 2  });  // Employee of an other store
                c.Users.Add(new User() { Name="user3", Storeid = null  });  // no employer, such as public user
                c.SaveChanges();

            }
            
        }

    }
}
