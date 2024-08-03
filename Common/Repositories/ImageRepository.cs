using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;
using KanzApi.Common.Repositories;

namespace KanzApi.Product.Repositories;

public class ImageRepository(CommonDbContext context)
: CrudRepository<Image, long?>(context, context.Images), IImageRepository
{ }
