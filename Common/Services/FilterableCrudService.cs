using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using System.Linq.Expressions;

namespace KanzApi.Common.Services;

public abstract class FilterableCrudService<T, ID>(ICrudRepository<T, ID> repository)
: CrudService<T, ID>(repository), IFilterableCrudService<T, ID> where T : CommonEntity<ID>
{

    protected abstract Expression<Func<T, bool>> Filter(Expression<Func<T, bool>>? predicate);

    public override int RemoveAllByPredicate(Expression<Func<T, bool>> predicate)
    {
        return base.RemoveAllByPredicate(Filter(predicate));
    }

    public override T? FindById(ID id, Expression<Func<T, bool>>? predicate)
    {
        return base.FindById(id, Filter(predicate));
    }

    public override T? FindByPredicate(Expression<Func<T, bool>> predicate)
    {
        return base.FindByPredicate(Filter(predicate));
    }

    public override PaginatedEntity<T> FindAll(Page page, Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        return base.FindAll(page, Filter(predicate), sort);
    }

    public override IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate, Sort? sort)
    {
        return base.FindAll(Filter(predicate), sort);
    }
}
