using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public interface IShippingMethodRepository : ICrudRepository<ShippingMethod, int?> { }
