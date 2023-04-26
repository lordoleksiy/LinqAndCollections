using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Infrastructure;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.BLL.Services;

public class TaskService : BaseService<TaskDTO, Task>, ITaskService
{
    public override IUnitOfWork UnitOfWork { get; }
    public override IRepository<Task> Repository { get; }
    public override Mapper Mapper { get; }
    public IAutoMapper AutoMapper { get; }
    public TaskService(IUnitOfWork unitOfWork, IAutoMapper autoMapper) : base(unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = unitOfWork.TaskRepository;
        Mapper = autoMapper.TaskMapper;
        AutoMapper = autoMapper;
    }
    public override async Task<TaskDTO> Update(TaskDTO task)
    {
        if (task == null)
        {
            throw new ValidationException("Invalid request body!", 400);
        }

        var entity = await Repository.GetById(task.Id);

        if (entity == null)
        {
            throw new ValidationException("No task with such Id!", 404);
        }

        var performer = await UnitOfWork.UserRepository.GetById(task.PerformerId);
        if (performer == null) 
        {
            throw new ValidationException("No performer with such id!", 404);
        }

        entity.Name = task.Name;
        entity.PerformerId = task.PerformerId;
        entity.Description = task.Description;
        entity.State = AutoMapper.StateMapper(task.State);
        entity.FinishedAt = task.FinishedAt;
        Repository.Update(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<TaskDTO>(entity);
    }

    public async override Task<TaskDTO> Create(TaskDTO item)
    {
        var performer = await UnitOfWork.UserRepository.GetById(item.PerformerId);
        var project = await UnitOfWork.ProjectRepository.GetById(item.ProjectId);

        if (performer == null || project == null)
        {
            throw new ValidationException("Invalid parameters", 400);
        }
        else
        {
            return await base.Create(item);
        }
    }
}
