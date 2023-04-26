using System.Linq.Expressions;

namespace CollectionsAndLinq.DAL.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    public Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] include);
    void Create(T item);
    void Update(T item);
    void Delete(T entity);
}
