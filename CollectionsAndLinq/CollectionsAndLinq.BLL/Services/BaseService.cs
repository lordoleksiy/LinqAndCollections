using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.BLL.Infrastructure;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.BLL.Services;

public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
    where TDto : class
    where TEntity: IEntity
{
    public virtual IUnitOfWork UnitOfWork { get; }
    public virtual IRepository<TEntity> Repository { get; }
    public  virtual Mapper Mapper { get; }


    public BaseService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    public virtual async Task<TDto> Create(TDto item)
    {
        if (item == null)
        {
            throw new ValidationException("Invalid request body!", 400);
        }

        var entity = Mapper.Map<TEntity>(item);
        Repository.Create(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<TDto>(entity);
    }

    public async Task<TDto> Delete(int id)
    {
        var entity = await Repository.GetById(id);
        if (entity == null)
        {
            throw new ValidationException("No entity with such Id!", 404);
        }

        Repository.Delete(entity);
        await UnitOfWork.SaveAsync();
        return Mapper.Map<TDto>(entity);
    }

    public async Task<IEnumerable<TDto>> GetAll()
    {
        return Mapper.Map<IEnumerable<TDto>>(await Repository.GetAll());
    }

    public async Task<TDto> GetValueById(int id)
    {
        var entity = await Repository.GetById(id);
        if (entity == null)
        {
            throw new ValidationException("No entity with such Id!", 404);
        }
        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> Update(TDto item)
    {
        return await Task.FromResult(item);
    }
}
