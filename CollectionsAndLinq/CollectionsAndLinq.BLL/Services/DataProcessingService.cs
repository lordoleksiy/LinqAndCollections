
using System.Linq.Expressions;
using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;
using CollectionsAndLinq.DAL.Repositories;
using Task = System.Threading.Tasks.Task;

namespace CollectionsAndLinq.BL.Services;

public class DataProcessingService : IDataProcessingService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAutoMapper mapper;
    public DataProcessingService(IUnitOfWork unitOfWork, IAutoMapper autoMapper)
    {
        this.unitOfWork = unitOfWork;
        mapper = autoMapper;
    }

    public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
    {
        var projects = (await unitOfWork.ProjectRepository.Get(
            a => a.AuthorId == userId,
            include: a => a.Tasks))
            .ToDictionary(
            x => $"{x.Id}: {x.Name}",
            x => x.Tasks.Count());

        return projects;
    }
    public async Task<List<(int, string)>> GetProjectsByTeamSizeAsync(int teamSize)
    {
        var projects = (await unitOfWork.ProjectRepository
            .Get(
            a => a.Team.Users.ToList().Count() > teamSize,
            include: a => a.Team.Users))
            .Select(a => (a.Id, a.Name));

        return projects.ToList();
    }

    public async Task<List<TaskDTO>> GetCapitalTasksByUserIdAsync(int userId)
    {
        var tasks = (await unitOfWork.TaskRepository.Get(a => a.PerformerId == userId))
            .Where(a => char.IsUpper(a.Name[0]));

        return mapper.TaskMapper.Map<IEnumerable<TaskDTO>>(tasks).ToList();
    }

    public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
    {
        var teams = (await unitOfWork.TeamRepository.Get(
            a => a.Users.All(a => a.BirthDay.Year < year),
            a => a.OrderBy(a => a.Name),
            a => a.Users))
            .Select(a => new TeamWithMembersDto(a.Id, a.Name, mapper.UserMapper.Map<IEnumerable<UserDTO>>(a.Users).ToList()));

        return teams.ToList();
    }

    public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
    {
        var query = await unitOfWork.UserRepository.Get(
            orderBy: a => a.OrderBy(a => a.FirstName),
            include: a => a.Tasks.OrderByDescending(a => a.Name.Length));
            
        var res = query.Select(a => new UserWithTasksDto(a.Id, a.FirstName, a.LastName, a.Email, a.RegisteredAt, a.BirthDay, 
            mapper.TaskMapper.Map<IEnumerable<TaskDTO>>(a.Tasks).ToList())).ToList();

        return res;

    }

    public async Task<UserInfoDto> GetUserInfoAsync(int userId)
    {
        Expression<Func<User, object>>[] include =
        {
            p => p.Projects,
            p => p.Tasks,
        };

        var user = (await unitOfWork.UserRepository.Get(
            a => a.Id == userId,
            include: include)).FirstOrDefault();

        var lastProject = user.Projects.LastOrDefault();

        var res = new UserInfoDto(
                mapper.UserMapper.Map<UserDTO>(user),
                mapper.ProjectMapper.Map<ProjectDTO>(lastProject),
                lastProject is null? 0: lastProject.Tasks.Count(),
                user.Tasks.Count(a => a.PerformerId == user.Id && a.State != TaskState.Done),
                mapper.TaskMapper.Map<TaskDTO>(user.Tasks.MaxBy(a => a.FinishedAt.HasValue ? a.FinishedAt - a.CreatedAt : DateTime.Now - a.CreatedAt))
            );

        return res;
    }

    public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
    {
        Expression<Func<Project, object>>[] include =
        {
            p => p.Tasks,
            p => p.Team.Users
        };
        var projects = await unitOfWork.ProjectRepository.Get(include: a => a.Tasks);

        var res = projects.Select(a => new ProjectInfoDto(
            mapper.ProjectMapper.Map<ProjectDTO>(a),
            mapper.TaskMapper.Map<TaskDTO>(a.Tasks.MaxBy(a => a.Description is null? 0: a.Description.Length)),
            mapper.TaskMapper.Map<TaskDTO>(a.Tasks.MinBy(a => a.Name.Length)),
            a.Team is null? null: a.Team.Users.Count()
            ));

        return res.ToList();
    }

    public Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        return null;
    }
}
