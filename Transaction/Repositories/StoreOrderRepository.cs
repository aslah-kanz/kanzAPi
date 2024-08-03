using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class StoreOrderRepository(CommonDbContext context)
: CrudRepository<StoreOrder, Guid?>(context, context.StoreOrders), IStoreOrderRepository
{ }
