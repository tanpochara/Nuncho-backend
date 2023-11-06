using Agoda.IoC.Core;
using Nuncho.WebApi.constants;
using Nuncho.WebApi.Model;
using Nuncho.WebApi.repository;
using Task = Nuncho.WebApi.entities.Task;
using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.service;

[RegisterTransient]
public class TaskService
{
    private readonly TaskRepository _taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        this._taskRepository = taskRepository;
    }
    
    public async Task<IEnumerable<Task>> GetTasksByProject(int projectId)
    {
        return await _taskRepository.GetTaskByProjectId(projectId);
    }

    public async Task<Task> CreateTask(int projectId, CreateTaskBody task)
    {
        var newTask = new Task()
        {
            Title = task.title,
            Description = task.description,
            Status = task.status ?? TaskStatus.NotStarted,
            Priority = task.priority ?? TaskPriority.Normal,
            BelongToId = projectId
        };
        return await _taskRepository.CreateTask(projectId, newTask);
    } 
    
    public async Task<Task> UpdateTaskStatus(int taskId, TaskStatus status)
    {
        return await _taskRepository.UpdateTaskStatus(taskId, status);
    }
}