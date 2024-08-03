using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class WebsiteProfileRepository(CommonDbContext context)
: CrudRepository<WebsiteProfile, int?>(context, context.WebsiteProfiles), IWebsiteProfileRepository
{ }
