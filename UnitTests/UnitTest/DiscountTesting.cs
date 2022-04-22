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

        [SetUp]
        public void Setup() 
        {
            
        }


        [Test]
        [Order(1)] // run this first
        public async Task IssueBill_ifNotExists_ReturnsNotFound()
        {
            // arrange
            var InvoiceRepoStub = new Mock<IInvoiceRepo>();
            var CustomerRepoStub = new Mock<ICustomerRepo>();
            InvoiceRepoStub.Setup(x => x.GetById(It.IsAny<int>())).Returns((Invoice)null); // GetById must be Mocked here. This is a unit test.
            var loggerStub = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(CustomerRepoStub.Object, InvoiceRepoStub.Object, loggerStub.Object);

            // act
            IActionResult result = controller.IssueBill(0); // pass any value (not important)
            var okResult = (IStatusCodeActionResult)result;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }


        [Test]
        [Order(2)]
        public async Task IssueBill_ifAlreadyIssued_ReturnsBadRequest()
        {
            // arrange
            var InvoiceRepoStub = new Mock<IInvoiceRepo>();
            var CustomerRepoStub = new Mock<ICustomerRepo>();

            Invoice i = new Invoice() { Statusid = InvoiceStatus.issued };
            InvoiceRepoStub.Setup(x => x.GetById(It.IsAny<int>())).Returns(i);
            var loggerStub = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(CustomerRepoStub.Object, InvoiceRepoStub.Object, loggerStub.Object);

            // act
            IActionResult result = controller.IssueBill(0); // pass any value (not important)
            var okResult = (IStatusCodeActionResult)result;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode); 
        }

        [Test]
        [Order(3)]
        public async Task IssueBill_ifCancelled_ReturnsBadRequest()
        {
            // arrange
            var InvoiceRepoStub = new Mock<IInvoiceRepo>();
            var CustomerRepoStub = new Mock<ICustomerRepo>();

            Invoice i = new Invoice() { Statusid = InvoiceStatus.cancelled };
            InvoiceRepoStub.Setup(x => x.GetById(It.IsAny<int>())).Returns(i);
            var loggerStub = new Mock<ILogger<DiscountController>>();
            var controller = new DiscountController(CustomerRepoStub.Object, InvoiceRepoStub.Object, loggerStub.Object);

            // act
            IActionResult result = controller.IssueBill(0); // pass any value (not important)
            var okResult = (IStatusCodeActionResult)result;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }


        [Test]
        [Order(2)]
        [TestCase(CustomerType.AnEmployee, 100, ExpectedResult = 70)] // 30 percent discount rule
        [TestCase(CustomerType.AnEmployee, 1, ExpectedResult = 0.7)]  // 30 percent discount rule
        [TestCase(CustomerType.AnEmployee, 0, ExpectedResult = 0)]    // 30 percent discount rule
        [TestCase(CustomerType.AnAffiate,  100, ExpectedResult = 90)] // 10 percent discount rule
        [TestCase(null,                    50, ExpectedResult = 50)] 
        [TestCase(null,                    100, ExpectedResult = 95)] // 5 discount for each 100 on the bill
        [TestCase(null,                    200, ExpectedResult = 190)] // 5 discount for each 100 on the bill
        [TestCase(null,                    210, ExpectedResult = 200)] // 5 discount for each 100 on the bill
        [TestCase(null,                    0,   ExpectedResult = 0)] // 0 -> 0
        public double GetDiscount_forACustomerType_DiscountMustBeGivenValue(int customertype, double billamount)
        {
            Customer c = new Customer() { TypeId = customertype, CreateTime = DateTime.Now };
            return DiscountLogic.GetDiscount(c, billamount);
        }


    }
}