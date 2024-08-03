using System.Linq.Expressions;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Repositories;

public abstract class CrudRepository<T, ID>(CommonDbContext context, DbSet<T> set)
: ICrudRepository<T, ID> where T : CommonEntity<ID>
{

    protected readonly CommonDbContext _context = context;

    protected readonly DbSet<T> _set = set;

    public T Add(T entity)
    {
        return _set.Add(entity).Entity;
    }

    public T Edit(T entity)
    {
        return _set.Update(entity).Entity;
    }

    public T Remove(T entity)
    {
        return _set.Remove(entity).Entity;
    }

    public int RemoveAllByPredicate(Expression<Func<T, bool>> predicate)
    {
        IQueryable<T> query = _set.Where(predicate);
        return query.ExecuteDelete();
    }

    public T? FindById(ID id, Expression<Func<T, bool>>? predicate)
    {
        return predicate != null
        ? _set.Where(predicate.And(arg => arg.Id!.Equals(id))).SingleOrDefault()
        : _set.Find(id);
    }

    public T? FindById(ID id)
    {
        return FindById(id, null);
    }

    public T? FindByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _set.Where(predicate).SingleOrDefault();
    }

    public PaginatedEntity<T> FindAll(Page page, Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        IQueryable<T> query = _set;
        if (predicate != null) query = query.Where(predicate);

        int count = query.Count();

        if (sort != null) query = query.Sort(sort);

        // fix wrong paging as its not start from 1 (found from UI)
        // if 0 is received, we still return the result from page 1
        var currentIndex = (page.Index < 1) ? 1 : page.Index;

        IEnumerable<T> entities = [.. query
        .Skip((currentIndex - 1) * page.Size)
        .Take(page.Size)];

        return new PaginatedEntity<T>(entities, page.Index, page.Size, count);
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        IQueryable<T> query = _set;
        if (predicate != null) query = query.Where(predicate);
        if (sort != null) query = query.Sort(sort);

        return [.. query];
    }

    public IEnumerable<T> FindAll(Sort? sort)
    {
        return FindAll(null, sort);
    }

    public IEnumerable<T> FindAll()
    {
        return FindAll(null);
    }

    public int Count(Expression<Func<T, bool>>? predicate)
    {
        IQueryable<T> query = _set;
        if (predicate != null) query = query.Where(predicate);

        return query.Count();
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }
}
