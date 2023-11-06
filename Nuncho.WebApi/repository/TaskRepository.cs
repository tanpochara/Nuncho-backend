using Agoda.IoC.Core;
using Microsoft.EntityFrameworkCore;
using Nuncho.WebApi.database;
using Task = Nuncho.WebApi.entities.Task;
using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.repository;

[RegisterTransient]
public class TaskRepository
{
    private readonly NunchoDatabaseContext _dbContext;
    
    public TaskRepository(NunchoDatabaseContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Task>> GetAllTasks()
    {
        return await this._dbContext.Tasks.ToListAsync();
    }
    
    public async Task<IEnumerable<Task>> GetTaskByProjectId(int projectId)
    {
        return await this._dbContext.Tasks
            .Where(t => t.BelongToId == projectId)
            .ToListAsync();
    }
    
    public async Task<Task> CreateTask(int projectId, Task task)
    {
        await this._dbContext.Tasks.AddAsync(task);
        await this._dbContext.SaveChangesAsync();
        return task;
    }
    
    public async Task<Task> UpdateTaskStatus(int taskId, TaskStatus status)
    {
        var task = await this._dbContext.Tasks.FindAsync(taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }
        task.Status = status;
        await this._dbContext.SaveChangesAsync();
        return task;
    }
}