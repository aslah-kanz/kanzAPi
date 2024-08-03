using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Repositories;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class StoreOrderFilterableService(IStoreOrderRepository repository, ISessionService sessionService)
: FilterableCrudService<StoreOrder, Guid?>(repository), IStoreOrderFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    protected override Expression<Func<StoreOrder, bool>> Filter(Expression<Func<StoreOrder, bool>>? predicate)
    {
        int? principalId = _sessionService.CurrentPrincipalId();
        if (principalId != null)
        {
            Expression<Func<StoreOrder, bool>> filterPredicate = StoreOrder
            .QPrincipalIdEquals((int)principalId)
            .Or(StoreOrder.QPrincipalIdsContains((int)principalId));
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }

        return predicate ?? Expressions.True<StoreOrder>();
    }
}
