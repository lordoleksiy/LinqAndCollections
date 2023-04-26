using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CollectionsAndLinq.DAL.DB;
using CollectionsAndLinq.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DataContext Db;
    public Repository(DataContext dataContext)
    {
        Db = dataContext;
    }

    public void Create(T item)
    {
        Db.Set<T>().Add(item);
    }

    public void Delete(T entity)
    {
        Db.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] include)
    {
        IQueryable<T> query = Db.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            foreach (var item in include)
            {
                query = query.Include(item);
            }
        }


        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Db.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await Db.Set<T>().FindAsync(id);
    }

    public void Update(T item)
    {
        Db.Entry(item).State = EntityState.Modified;
    }
}
