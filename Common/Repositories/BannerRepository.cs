using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class BannerRepository(CommonDbContext context)
: CrudRepository<Banner, int?>(context, context.Banners), IBannerRepository
{ }
