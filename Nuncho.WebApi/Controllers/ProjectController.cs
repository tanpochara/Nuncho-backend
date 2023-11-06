using Microsoft.AspNetCore.Mvc;
using Nuncho.WebApi.Model;
using Nuncho.WebApi.service;
using Task = Nuncho.WebApi.entities.Task;

namespace Nuncho.WebApi.Controllers;

[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly JwtService _jwtService;
    
    public ProjectController(ProjectService projectService, JwtService jwtService)
    {
        this._projectService = projectService;
        this._jwtService = jwtService;
    }
    
    [HttpGet("hello")]
    public IActionResult GetHello()
    {
        var test = Request.Headers;
        return Ok(test);
    }
    
    [HttpGet("projects")]
    public async Task<IActionResult> GetAllProjects()
    {
        var result = await this._projectService.GetAllProjects();
        return Ok(result);
    }
    
    [HttpGet("projectByUserId")]
    public async Task<IActionResult> GetProjectByUserId()
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var user = await this._jwtService.DecodeJwtToken(token);
        if (user != null)
        {
            var result = await this._projectService.GetProjectByUserId(user.Id);
            return Ok(result);
        }
        return BadRequest("User not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectBody projectBody)
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var user = await this._jwtService.DecodeJwtToken(token);
        if (user != null)
        {
            var result = await this._projectService.CreateProject(user.Id, projectBody.title, projectBody.description);
            return Ok(result);
        }
        return BadRequest("User not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var user = await this._jwtService.DecodeJwtToken(token);
        if (user != null)
        {
            var result = await this._projectService.DeleteProject(id, user.Id);
            return Ok(result);
        }
        return BadRequest("User not found");
    }
}