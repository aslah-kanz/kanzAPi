using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class CustomerOrderItemRepository(CommonDbContext context)
: CrudRepository<CustomerOrderItem, Guid?>(context, context.CustomerOrderItems), ICustomerOrderItemRepository
{ }
