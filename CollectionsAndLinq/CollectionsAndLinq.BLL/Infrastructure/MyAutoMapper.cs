using System.Reflection;
using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.WebApi.Infrastructure;

public class MyAutoMapper: IAutoMapper
{
    private Mapper? _projectMapper;
    private Mapper? _taskMapper;
    private Mapper? _teamMapper;
    private Mapper? _userMapper;
    private Mapper? _teamWithUserMapper;
    public  Mapper ProjectMapper 
    {
        get
        {
            if (_projectMapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(x => x.Team, opt => opt.Ignore())
                .ForMember(x => x.Tasks, opt => opt.Ignore())
                .ReverseMap());
                _projectMapper = new Mapper(config);
            }
            return _projectMapper;
        }

    }

    public Mapper TeamMapper
    {
        get
        {
            if (_teamMapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TeamDTO, Team>()
                .ForMember(x => x.Projects, opt => opt.Ignore())
                .ForMember(x => x.Users, opt => opt.Ignore())
                .ReverseMap());
                _teamMapper = new Mapper(config);
            }
            return _teamMapper;
        }

    }
    public Mapper TeamWithUsersMapper
    {
        get
        {
            if (_teamWithUserMapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TeamWithMembersDto, Team>()
                .ForMember(x => x.Projects, opt => opt.Ignore())
                .ForMember(x => x.Users, opt => opt.MapFrom(a => UserMapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(a.Members)))
                .ForMember(x => x.CreatedAt, IMappingOperationOptions => IMappingOperationOptions.Ignore())
                .ReverseMap());
                _teamWithUserMapper = new Mapper(config);
            }
            return _teamWithUserMapper;
        }
    }

    public Mapper TaskMapper
    {
        get
        {
            if (_taskMapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Task>()
                .ForMember(x => x.Project, opt => opt.Ignore())
                .ForMember(x => x.Performer, opt => opt.Ignore())
                .ForMember(x => x.State, opt => opt.MapFrom(src => StateMapper(src.State)))
                .ReverseMap());
                _taskMapper = new Mapper(config);
            }
            return _taskMapper;
        }

    }
    public TaskState StateMapper(string taskState)
    {
        return taskState switch
        {
            "To Do" => TaskState.ToDo,
            "Canceled" => TaskState.Canceled,
            "Done" => TaskState.Done,
            "In Progress" => TaskState.InProgress,
            _ => TaskState.ToDo
        };
    }

    public Mapper UserMapper
    {
        get
        {
            if (_userMapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()
                .ForMember(x => x.Projects, opt => opt.Ignore())
                .ForMember(x => x.Team, opt => opt.Ignore())
                .ForMember(x => x.Tasks, opt => opt.Ignore())
                .ReverseMap());
                _userMapper = new Mapper(config);
            }
            return _userMapper;
        }

    }
}
