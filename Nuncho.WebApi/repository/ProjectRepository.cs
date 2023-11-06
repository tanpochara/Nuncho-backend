using Agoda.IoC.Core;
using Microsoft.EntityFrameworkCore;
using Nuncho.WebApi.database;
using Nuncho.WebApi.entities;

namespace Nuncho.WebApi.repository;

[RegisterTransient]
public class ProjectRepository
{
    private readonly NunchoDatabaseContext _dbContext;

    public ProjectRepository(NunchoDatabaseContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await this._dbContext.Projects
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Project>> GetProjectByOwnerId(int ownerId)
    {
        return await this._dbContext.Projects
            .Where(p => p.OwnerId == ownerId)
            .Include(p => p.Tasks)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectByUserId(int id)
    {
        return await this._dbContext.Projects
            .Where(p => p.OwnerId == id)
            .Include(p => p.Tasks)
            .ToListAsync();
    }

    public async Task<Project> CreateProject(Project project)
    {
        await this._dbContext.Projects.AddAsync(project);
        await this._dbContext.SaveChangesAsync();
        return project;
    }
    
    public async Task<Project> DeleteProject(Project project)
    {
        this._dbContext.Projects.Remove(project);
        await this._dbContext.SaveChangesAsync();
        return project;
    }
}