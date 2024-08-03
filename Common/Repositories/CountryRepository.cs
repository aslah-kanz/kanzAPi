using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class CountryRepository(CommonDbContext context)
: CrudRepository<Country, int?>(context, context.Countries), ICountryRepository
{ }
