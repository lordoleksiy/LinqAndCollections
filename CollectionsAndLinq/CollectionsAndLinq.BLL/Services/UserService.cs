using AutoMapper;
using CollectionsAndLinq.BLL.DTOs;
using CollectionsAndLinq.BLL.Infrastructure;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.BLL.Services;

public class UserService : BaseService<UserDTO, User>, IUserService
{
    public override IUnitOfWork UnitOfWork { get; }
    public override IRepository<User> Repository { get; }
    public override Mapper Mapper { get; }
    public UserService(IUnitOfWork unitOfWork, IAutoMapper autoMapper) : base(unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = unitOfWork.UserRepository;
        Mapper = autoMapper.UserMapper;
    }
    public override async Task<UserDTO> Update(UserDTO user)
    {
        if (user == null)
        {
            throw new ValidationException("Invalid request body!", 400);
        }

        var entity = await Repository.GetById(user.Id);

        if (entity == null)
        {
            throw new ValidationException("No user with such Id!", 404);
        }
        if (user.TeamId.HasValue)
        {
            var team = await UnitOfWork.TeamRepository.GetById(user.TeamId.Value);
            if (team == null)
            {
                throw new ValidationException("No team with such id!", 404);
            }
        }
        var queryuser = await Repository.Get(a => a.Email.Equals(user.Email));
        if (queryuser.Any() && queryuser.First().Id != user.Id)
        {
            throw new ValidationException("User with such email is already registered!", 400);
        }

        entity.TeamId = user.TeamId;
        entity.FirstName = user.FirstName;
        entity.LastName = user.LastName;
        entity.Email = user.Email;
        entity.BirthDay = user.BirthDay;
        Repository.Update(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<UserDTO>(entity);
    }
    public async override Task<UserDTO> Create(UserDTO item)
    {
        var user = await Repository.Get(a => a.Email.Equals(item.Email));
        if (user.Any())
        {
            throw new ValidationException("User with such email exists", 400);
        }
        if (item.TeamId.HasValue)
        {
            var team = await UnitOfWork.TeamRepository.GetById(item.TeamId.Value);
            if (team == null) 
            {
                throw new ValidationException("Invalid TeamId argument", 400);
            }
        }
        return await base.Create(item);
    }
}
