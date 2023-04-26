using CollectionsAndLinq.DAL.DB;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.DAL.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly DataContext _dataContext;
    private IRepository<Task> taskRepository;
    private IRepository<Team> teamRepository;
    private IRepository<User> userRepository;
    private IRepository<Project> projectRepository;
    private bool disposed;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public IRepository<Task> TaskRepository 
    { 
        get 
        {
            taskRepository ??= new Repository<Task>(_dataContext);
            return taskRepository;
        }
    }

    public IRepository<Team> TeamRepository
    {
        get
        {
            teamRepository ??= new Repository<Team>(_dataContext); 
            return teamRepository;
        }
    }

    public IRepository<User> UserRepository
    {
        get
        {
            userRepository ??= new Repository<User>(_dataContext);
            return userRepository;
        }
    }


    public IRepository<Project> ProjectRepository
    {
        get
        {
            projectRepository ??= new Repository<Project>(_dataContext);
            return projectRepository;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dataContext.Dispose();
        //Dispose(true);
        //GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
            disposed = true;
        }
    }
}
