using LibraryWebApi.Models.http;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserRequest userRequest)
    {
        var userId = await userService.RegisterUserAsync(userRequest);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
    {
        var token = await userService.LoginAsync(userRequest);
        if (token != null)
        {
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
}
