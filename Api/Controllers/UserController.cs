using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repo;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepo _Customers;

        public CustomerController(ICustomerRepo Customers)
        {
            _Customers = Customers;
        }



        [HttpGet("GetCustomers")]
        public async Task<IActionResult>GetCustomers()
        {
             return Ok(_Customers.GetCustomers());
        }

        [HttpGet("GetCustomers/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            return Ok(_Customers.GetById(id));
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(Customer Customer)
        {
            try
            {
                if (string.IsNullOrEmpty(Customer.Name))
                    return BadRequest("name can not be empty");
               
                return Ok( _Customers.CreateCustomer(Customer));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
