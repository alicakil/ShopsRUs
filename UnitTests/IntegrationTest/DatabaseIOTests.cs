using Api.Controllers;
using Api.Models;
using Api.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class DatabaseIOTests
    {
        [SetUp]
        public void Setup()
        {
            // Database Connection Check...
            Context c = new Context();
            if (!c.Database.CanConnect())
            {
                Assert.Ignore("DB Conncetion Failed!");
            }
        }

        [Test]
        [Order(1)] // run this first
        [TestCase(1000, 404)] // not existing invoice
        [TestCase(-1000, 404)] // not existing invoice
        [TestCase(1, 200)] // existing invoice
        public async Task GetInvoice_MustbeGivenResultCode(int Invoiceid, int HttpResultCode)
        {
            // arrange
            Context ctx = new Context(); // using real database connection.
            CustomerRepo c = new CustomerRepo(ctx);
            InvoiceRepo  i = new InvoiceRepo(ctx);
            var loggerStub = new Mock<ILogger<DiscountController>>();// we do integration test for datebase. so logger is not important here.
            var controller = new DiscountController(c, i, loggerStub.Object); 

            // act
            IActionResult result = controller.GetInvoice(Invoiceid); // database testing based on demo records. (to determine if repo works correctly)
            var okResult = (IStatusCodeActionResult)result;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(HttpResultCode, okResult.StatusCode);
        }
    }
}
