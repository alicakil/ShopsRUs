using NUnit.Framework;
using Api.Models;
using Api.Controllers;
using Api.Repo;
using Api.BusinessLogic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace UnitTests
{
    public class DiscountTesting
    {
        string API_URL = "https://localhost:7123/api";

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
        [TestCase(1000,  404)] // not existing customer
        [TestCase(-1000, 404)] // not existing customer
        [TestCase(1,     200)] // existing customer
        public async Task GetInvoice_MustbeGivenResultCode(int Customerid, int HttpResultCode)
        {
            // arrange
            var InvoiceRepoStub = new Mock<IInvoiceRepo>();
            var CustomerRepoStub = new Mock<ICustomerRepo>();
            InvoiceRepoStub.Setup(x => x.GetById(It.IsAny<int>())).Returns((Invoice)null);
            var loggerStub = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(CustomerRepoStub.Object, InvoiceRepoStub.Object, loggerStub.Object);

            // act
            IActionResult result = controller.GetInvoice(Customerid);
            var okResult = (IStatusCodeActionResult)result;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(HttpResultCode, okResult.StatusCode);
        }



        [Test]
        [Order(2)]
        [TestCase(CustomerType.AnEmployee, 100, ExpectedResult = 70)] // 30 percent discount rule
        [TestCase(CustomerType.AnEmployee, 1, ExpectedResult = 0.7)]  // 30 percent discount rule
        [TestCase(CustomerType.AnAffiate,  100, ExpectedResult = 90)] // 10 percent discount rule
        public double GetDiscount_forACustomerType_DiscountMustBeGivenValue(int customertype, double billamount)
        {
            Customer c = new Customer() { TypeId = customertype, CreateTime = DateTime.Now };
            return DiscountLogic.GetDiscount(c, billamount);
        }

       


        //[Test]
        //[Order(2)]
        //[TestCase(1, 1000, ExpectedResult = 650)] // 
        //public int GetDiscount_MustbeGivenResult(int Customerid, double billAmmount)
        //{
        //    // arrange
        //    var InvoiceRepoStub = new Mock<IInvoiceRepo>();
        //    var CustomerRepoStub = new Mock<ICustomerRepo>();
        //    InvoiceRepoStub.Setup(x => x.GetById(It.IsAny<int>())).Returns((Invoice)null);
        //    var loggerStub = new Mock<ILogger<DiscountController>>();
        //    var controller = new DiscountController(CustomerRepoStub.Object, InvoiceRepoStub.Object, loggerStub.Object);

        //    // act
        //    IActionResult result = controller.CreateBill(Customerid, billAmmount);
        //    var okResult = result as OkObjectResult;

        //    Assert.IsNotNull(okResult);

        //    var xx = okResult.Value;
        //    var resultcode = (IStatusCodeActionResult)result; // resultcode.StatusCode

        //    // assert
        //    return 650;
        //}

    }
}