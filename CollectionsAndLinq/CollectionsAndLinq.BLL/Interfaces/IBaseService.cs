using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.DAL.Interfaces;

namespace CollectionsAndLinq.BLL.Interfaces;

public interface IBaseService<TDto, TEntity> 
    where TDto : class
    where TEntity : IEntity
{
    IUnitOfWork UnitOfWork { get; }
    IRepository<TEntity> Repository { get; }
    Mapper Mapper { get; }
    public Task<TDto> GetValueById(int id);
    public Task<IEnumerable<TDto>> GetAll();
    public Task<TDto> Create(TDto item);
    public Task<TDto> Update(TDto item);
    public Task<TDto> Delete(int id);
}
