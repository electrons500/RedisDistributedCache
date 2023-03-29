using Microsoft.AspNetCore.Mvc;
using RedisDistributedCache.Data.Model;
using RedisDistributedCache.Data.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisDistributedCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult AddUser([FromBody] Users users)
        {
            bool result = _usersService.AddUserDataToCache(users);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created, users);
            }

            return BadRequest();
        }

        //get user by key
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult> GetUsers(int id)
        {
            var model = await _usersService.GetUsersAsync($"user{id}");
            if (model == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, model);
        }
    }
}
