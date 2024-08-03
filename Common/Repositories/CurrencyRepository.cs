using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class CurrencyRepository(CommonDbContext context)
: CrudRepository<Currency, int?>(context, context.Currencies), ICurrencyRepository
{ }
