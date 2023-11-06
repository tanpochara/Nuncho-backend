using Agoda.IoC.Core;
using Microsoft.EntityFrameworkCore;
using Nuncho.WebApi.database;
using Nuncho.WebApi.entities;

namespace Nuncho.WebApi.repository;

[RegisterTransient]
public class UserRepository
{
    private readonly NunchoDatabaseContext _dbContext;

    public UserRepository(NunchoDatabaseContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await this._dbContext.Users
            .Include(u => u.Projects)
            .ToListAsync();
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await this._dbContext.Users
            .Where(u => u.Email == username)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

    public async Task<User> Add(User user)
    {
        await this._dbContext.Users.AddAsync(user);
        await this._dbContext.SaveChangesAsync();
        return user;
    }
}