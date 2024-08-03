using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class ShippingMethodRepository(CommonDbContext context)
: CrudRepository<ShippingMethod, int?>(context, context.ShippingMethods), IShippingMethodRepository
{ }
