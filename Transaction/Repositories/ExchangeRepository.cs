using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class ExchangeRepository(CommonDbContext context)
: CrudRepository<Exchange, Guid?>(context, context.Exchanges), IExchangeRepository
{ }
