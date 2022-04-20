using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        public DiscountController( )
        {
        }

        [HttpGet("GetDiscount")]
        public async Task<IActionResult> GetDiscount()
        {
            return Ok("0"); // will be prepared..
        }

    }
}
