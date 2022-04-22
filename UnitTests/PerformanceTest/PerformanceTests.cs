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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PerformanceTest
{
    internal class PerformanceTests
    {

        [Test, Order(100)] // Last Test
        [TestCase(100, 5)] // Try 100 times, in 5 secs.
        public void AddInvoice_Speed_MustPassGivenLimit(int NrOfRequests, int MaxTimeInSec) // real database performance
        {
            // arrange
          
            Context ctx = new Context(); // using real database connection.
            CustomerRepo c = new CustomerRepo(ctx);
            InvoiceRepo i = new InvoiceRepo(ctx);
            var loggerStub = new Mock<ILogger<DiscountController>>(); 
            var controller = new DiscountController(c, i, loggerStub.Object);
            Random rand = new Random();

            // act

            var timer = Stopwatch.StartNew();

                for (int m = 0; m < NrOfRequests; m++)
                {
                    IActionResult result = controller.CreateBill(rand.Next(0, 5), rand.Next(0, 1000));
                    var okResult = (IStatusCodeActionResult)result;
                }

            timer.Stop();

            // assert
            TimeSpan time = timer.Elapsed;
            TimeSpan Max = TimeSpan.FromSeconds(MaxTimeInSec);


            if (time < Max)
                Assert.Pass("Passed with: " + time.Milliseconds.ToString() + "ms");
            else
                Assert.Fail("Login Performance failed. Time:" + time.Milliseconds.ToString() + "ms");
        }
    }
}
