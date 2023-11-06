using Microsoft.AspNetCore.Mvc;
using Nuncho.WebApi.Model;
using Nuncho.WebApi.service;
using Task = Nuncho.WebApi.entities.Task;

namespace Nuncho.WebApi.Controllers;

[Route("/api/task")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;
    
    public TaskController(TaskService taskService)
    {
        this._taskService = taskService;
    }
    
    [HttpGet("byproject/{id}")]
    public async Task<IActionResult> GetTasks(int id)
    {
        var tasks = await _taskService.GetTasksByProject(id);
        return Ok(tasks);
    }
    
    [HttpPost("byproject/{id}")]
    public async Task<IActionResult> CreateTask(int id, [FromBody] CreateTaskBody task)
    {
        var newTask = await this._taskService.CreateTask(id, task);
        return Ok(task);
    }
    
    [HttpPost("update/status/{id}")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] UpdateTaskBody body)
    {
        try
        {
            var task = await this._taskService.UpdateTaskStatus(id, body.Status);
            return Ok(task);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
}