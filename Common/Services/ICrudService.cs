using System.Linq.Expressions;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ICrudService<T, ID> where T : CommonEntity<ID>
{

    T Add(T entity);

    T Edit(T entity);

    T Remove(T entity);

    T RemoveById(ID id);

    int RemoveAllByPredicate(Expression<Func<T, bool>> predicate);

    T? FindById(ID id, Expression<Func<T, bool>>? predicate);

    T? FindById(ID id);

    T GetById(ID id);

    T? FindByPredicate(Expression<Func<T, bool>> predicate);

    PaginatedEntity<T> FindAll(Page page, Expression<Func<T, bool>>? predicate, Sort? sort);

    PaginatedEntity<T> FindAll(IPageableParam<T> param);

    IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate, Sort? sort);

    IEnumerable<T> FindAll(ISearchableParam<T> param);

    IEnumerable<T> FindAll(ISortableParam param);

    IEnumerable<T> FindAll();
}
