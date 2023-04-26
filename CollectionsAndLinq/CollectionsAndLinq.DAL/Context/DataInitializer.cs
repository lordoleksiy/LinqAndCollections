using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.DAL.DB;

public static class DataInitializer
{
    public static void FillInData(this ModelBuilder modelBuilder)
    {
        var teams = new List<Team>{ new Team { Id = 1, Name = "Best Team", CreatedAt = DateTime.Now } };
        var projects = new List<Project>{new Project
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
            Name = ".net linq proj",
            Description = "descr",
            CreatedAt = DateTime.Now,
            Deadline = DateTime.Today.AddDays(1)
        }};
        var users = new List<User>{new User
        {
            Id = 1,
            TeamId = 1,
            FirstName = "Vollan",
            LastName = "De Mort",
            Email = "volland77192@gmail.com",
            RegisteredAt = DateTime.Now.AddDays(-3),
            BirthDay= DateTime.Today.AddYears(-19)
        }};
        var tasks = new List<Task>{new Task
        {
            Id = 1,
            ProjectId = 1,
            PerformerId = 1,
            Name = "finish back",
            Description = "finish back immediately",
            State = TaskState.InProgress,
            CreatedAt = DateTime.Now.AddDays(-2)
        } };

        modelBuilder.Entity<Team>().HasData(teams);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Task>().HasData(tasks);
        modelBuilder.Entity<Project>().HasData(projects);
    }
}
