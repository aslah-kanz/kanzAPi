using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public class BrandRepository(CommonDbContext context)
: CrudRepository<Brand, int?>(context, context.Brands), IBrandRepository
{ }
