using KanzApi.Common.Services;

namespace KanzApi.Transaction.Services;

public interface ICustomerOrderItemService : ICrudService<Entities.CustomerOrderItem, Guid?>
{ }
