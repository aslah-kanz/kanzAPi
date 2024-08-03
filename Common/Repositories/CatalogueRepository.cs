using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class CatalogueRepository(CommonDbContext context)
: CrudRepository<Catalogue, int?>(context, context.Catalogues), ICatalogueRepository
{ }
