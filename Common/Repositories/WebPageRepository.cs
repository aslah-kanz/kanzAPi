using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class WebPageRepository(CommonDbContext context)
: CrudRepository<WebPage, int?>(context, context.WebPages), IWebPageRepository
{ }
