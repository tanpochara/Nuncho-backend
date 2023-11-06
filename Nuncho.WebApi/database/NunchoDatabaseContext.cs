using Agoda.IoC.Core;
using Microsoft.EntityFrameworkCore;
using Nuncho.WebApi.entities;
using Task = Nuncho.WebApi.entities.Task;

namespace Nuncho.WebApi.database;

public class NunchoDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseInMemoryDatabase("nuncho");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships in the database model
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.OwnerId);

        modelBuilder.Entity<Task>()
            .HasOne(t => t.BelongTo)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.BelongToId);
        
    }
    
}
