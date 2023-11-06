using Agoda.IoC.Core;
using Nuncho.WebApi.entities;
using Nuncho.WebApi.repository;
using Task = Nuncho.WebApi.entities.Task;

namespace Nuncho.WebApi.service;

[RegisterTransient]
public class ProjectService
{
    private readonly ProjectRepository _projectRepository;
    
    public ProjectService(ProjectRepository projectRepository)
    {
        this._projectRepository = projectRepository;
    }
    
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await this._projectRepository.GetAllProjects();
    }
    
    public async Task<IEnumerable<Project>> GetProjectByUserId(int userId)
    {
        return await this._projectRepository.GetProjectByUserId(userId);
    }
    
    public async Task<Project> CreateProject(int userId, string title, string description)
    {
        var newProject = new Project()
        {
            Title = title,
            Description = description,
            OwnerId = userId,
            Tasks = new List<Task>(),
        };
        return await this._projectRepository.CreateProject(newProject);
    }
    
    public async Task<Project> DeleteProject(int projectId, int userId)
    {
        var ownedProject = await this._projectRepository.GetProjectByUserId(userId);
        var project = ownedProject.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }
        return await this._projectRepository.DeleteProject(project);
    }
    
}