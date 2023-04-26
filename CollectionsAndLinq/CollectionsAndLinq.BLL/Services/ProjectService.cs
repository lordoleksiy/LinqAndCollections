using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Infrastructure;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace CollectionsAndLinq.BLL.Services;

public class ProjectService: BaseService<ProjectDTO, Project>, IProjectService
{
    public override IUnitOfWork UnitOfWork { get; }
    public override IRepository<Project> Repository { get; }
    public override Mapper Mapper { get; }
    public ProjectService(IUnitOfWork unitOfWork, IAutoMapper autoMapper): base(unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = unitOfWork.ProjectRepository;
        Mapper = autoMapper.ProjectMapper;
    }
    public override async Task<ProjectDTO> Update(ProjectDTO project)
    {
        if (project == null)
        {
            throw new ValidationException("Invalid request body!", 400);
        }

        var entity = await Repository.GetById(project.Id);

        if (entity == null)
        {
            throw new ValidationException("No project with such Id!", 404);
        }

        var reqproject = await Repository.Get(a => a.Name.Equals(project.Name));
        if (reqproject.Any() && !reqproject.First().Id.Equals(project.Id)) 
        {
            throw new ValidationException("Project with such name exists!", 400);
        }

        entity.Name = project.Name;
        entity.Description = project.Description;
        entity.Deadline = project.Deadline;
        Repository.Update(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<ProjectDTO>(entity);
    }
    public async override Task<ProjectDTO> Create(ProjectDTO item)
    {
        var project = await Repository.Get(a => a.Name.Equals(item.Name));
        var team = await UnitOfWork.TeamRepository.GetById(item.TeamId);
        var author = await UnitOfWork.UserRepository.GetById(item.AuthorId);
        if (project.Any() || team == null || author == null)
        {
            throw new ValidationException("Invalid parameters", 400);
        }
        else
        {
            return await base.Create(item);
        }
    }
}
