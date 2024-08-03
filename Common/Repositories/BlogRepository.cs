using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class BlogRepository(CommonDbContext context)
: CrudRepository<Blog, int?>(context, context.Blogs), IBlogRepository
{ }
