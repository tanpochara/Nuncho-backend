using Microsoft.AspNetCore.Mvc;
using Nuncho.WebApi.service;

namespace Nuncho.WebApi.Controllers;

[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        this._userService = userService;
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        Console.WriteLine(Request.Headers);
        var users = await this._userService.GetAllUsers();
        return Ok(users);
    }
}