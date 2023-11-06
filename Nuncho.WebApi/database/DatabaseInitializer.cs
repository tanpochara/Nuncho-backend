using Nuncho.WebApi.constants;
using Nuncho.WebApi.entities;
using Task = Nuncho.WebApi.entities.Task;
using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.database;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.Generic;

public static class DatabaseInitializer
{
    public static void Initialize(NunchoDatabaseContext context)
    {
        context.Database.EnsureCreated(); // Ensure the database is created

        // Check if there are any users in the database
        if (!context.Users.Any())
        {
            // Add initial data for the User entity
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Password = "password123"
            };
            context.Users.Add(user);

            // Add more initial data for other entities (Projects, Tasks, Tags, etc.)
            if (!context.Projects.Any())
            {
                var project = new Project
                {
                    OwnerId = user.Id,
                    Title = "Sample Project",
                    Description = "A sample project description"
                };
                context.Projects.Add(project);

                if (!context.Tasks.Any())
                {
                    var task = new Task()
                    {
                        BelongToId = project.Id,
                        Title = "Sample Task",
                        Description = "A sample task description",
                        Status = TaskStatus.NotStarted,
                        Priority = TaskPriority.Normal,
                        tags = "dev"
                    };
                    context.Tasks.Add(task);
                }
            }

            context.SaveChanges();
        }
    }
}
