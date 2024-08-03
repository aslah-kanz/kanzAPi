using System.Linq.Expressions;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Repositories;

public interface ICrudRepository<T, ID> where T : CommonEntity<ID>
{
    T Add(T entity);

    T Edit(T entity);

    T Remove(T entity);

    int RemoveAllByPredicate(Expression<Func<T, bool>> predicate);

    T? FindById(ID id, Expression<Func<T, bool>>? predicate);

    T? FindById(ID id);

    T? FindByPredicate(Expression<Func<T, bool>> predicate);

    PaginatedEntity<T> FindAll(Page page, Expression<Func<T, bool>>? predicate, Sort? sort);

    IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate, Sort? sort);

    IEnumerable<T> FindAll(Sort? sort);

    IEnumerable<T> FindAll();

    int Count(Expression<Func<T, bool>>? predicate);

    int Commit();
}
