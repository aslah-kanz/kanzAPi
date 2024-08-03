using KanzApi.Common.Services;
using KanzApi.Transaction.Repositories;

namespace KanzApi.Transaction.Services;

public class CustomerOrderItemService(ICustomerOrderItemRepository repository)
: CrudService<Entities.CustomerOrderItem, Guid?>(repository), ICustomerOrderItemService
{ }
