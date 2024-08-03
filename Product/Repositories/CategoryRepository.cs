using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public class CategoryRepository(CommonDbContext context)
: CrudRepository<Category, int?>(context, context.Categories), ICategoryRepository
{ }
