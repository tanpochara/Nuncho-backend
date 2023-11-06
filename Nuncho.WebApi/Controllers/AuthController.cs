using Microsoft.AspNetCore.Mvc;
using Nuncho.WebApi.Model;
using Nuncho.WebApi.service;

namespace Nuncho.WebApi.Controllers;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        this._authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginBody loginBody)
    {
        try
        {
            var jwt = await this._authService.SignIn(loginBody.Email, loginBody.Password);
            return Ok(jwt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterBody registerBody)
    {
        try
        {
            var jwt = await this._authService.SignUp(
                email: registerBody.email,
                password: registerBody.password,
                name: registerBody.name);
            return Ok(jwt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
}