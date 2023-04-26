using CollectionsAndLinq.DAL.Entities;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.DAL.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IRepository<Project> ProjectRepository { get; }
    IRepository<Task> TaskRepository { get; }
    IRepository<Team> TeamRepository { get; }
    IRepository<User> UserRepository { get; }
    public Task<int> SaveAsync();
}
