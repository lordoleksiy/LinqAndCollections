using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Infrastructure;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.BLL.Services;

public class TeamService : BaseService<TeamDTO, Team>, ITeamService
{
    public override IUnitOfWork UnitOfWork { get; }
    public override IRepository<Team> Repository { get; }
    public override Mapper Mapper { get; }
    public IAutoMapper AutoMapper { get; }
    public TeamService(IUnitOfWork unitOfWork, IAutoMapper autoMapper) : base(unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = unitOfWork.TeamRepository;
        Mapper = autoMapper.TeamMapper;
        AutoMapper = autoMapper;
    }
    public override async Task<TeamDTO> Update(TeamDTO team)
    {
        if (team == null)
        {
            throw new ValidationException("Invalid request body!", 400);
        }

        var entity = await Repository.GetById(team.Id);

        if (entity == null)
        {
            throw new ValidationException("No team with such Id!", 404);
        }

        entity.Name = team.Name;
        Repository.Update(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<TeamDTO>(entity);
    }

    public async override Task<TeamDTO> Create(TeamDTO item)
    {
        var team = await Repository.Get(a => a.Name.Equals(item.Name));

        if (team.Any())
        {
            throw new ValidationException("Invalid parameters", 400);
        }
        else
        {
            return await base.Create(item);
        }
    }
}
