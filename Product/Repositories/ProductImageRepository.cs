using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public class ProductImageRepository(CommonDbContext context)
: CrudRepository<ProductImage, int?>(context, context.ProductImages), IProductImageRepository
{ }
