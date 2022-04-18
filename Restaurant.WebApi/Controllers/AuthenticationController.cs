using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Services;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    //public class AuthenticationController : ControllerBase
    //{
    //    private readonly IUserService _userService;

    //    public AuthenticationController(IUserService userService)
    //    {
    //        _userService = userService;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Authenticate([FromBody] UserViewModel user)
    //    {
    //        if(string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
    //        {
    //            return BadRequest("The username or password are missing.");
    //        }

    //        var token = await _userService.AuthenticateUser("User1", "Parola1");
    //        return Ok(token);
    //    }
    //}
}
