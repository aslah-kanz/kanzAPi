using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public class ProductAttributeRepository(CommonDbContext context)
: CrudRepository<ProductAttribute, Guid?>(context, context.ProductAttributes), IProductAttributeRepository
{ }
