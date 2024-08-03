using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public interface ICustomerOrderRepository : ICrudRepository<CustomerOrder, Guid?> { }
