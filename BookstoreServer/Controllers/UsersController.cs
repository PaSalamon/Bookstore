using Bookstore.DbServices;
using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> RegisterUser(UserModel user)
        {
            var dbUser = await userService.RegisterUser(user);

            return StatusCode(StatusCodes.Status200OK, dbUser);
        }

        [HttpGet ("{email}/{password}") ]
        public async Task<IActionResult> GetUserByEmailAndPassword(string email, string password)
        {
            var dbUser = await userService.GetUserByEmailAndPassword(email,password);

            if (dbUser.Email == null || dbUser.Password == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Email or Password are wrong");
            }
            else
            return StatusCode(StatusCodes.Status200OK, "Authorized");
        }
    }
}
