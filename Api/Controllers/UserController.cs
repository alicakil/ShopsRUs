using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repo;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Context c;
        IUserRepo _users;

        public UserController(IUserRepo users)
        {
            c = new Context();
            _users = users;
        }



        [HttpGet("GetUsers")]
        public async Task<IActionResult>GetUsers()
        {
             return Ok(await _users.GetUsersAsync());
        }

        [HttpGet("GetUsers/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _users.GetByIdAsnc(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Name))
                    return BadRequest("name can not be empty");
               
                return Ok(await _users.CreateUserAsync(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
