using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public class SaleItemRepository(CommonDbContext context)
: CrudRepository<SaleItem, long?>(context, context.SaleItems), ISaleItemRepository
{ }
