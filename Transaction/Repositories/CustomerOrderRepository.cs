using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class CustomerOrderRepository(CommonDbContext context)
: CrudRepository<CustomerOrder, Guid?>(context, context.CustomerOrders), ICustomerOrderRepository
{ }
