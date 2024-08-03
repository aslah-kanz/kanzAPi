using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class StoreRepository(CommonDbContext context)
: CrudRepository<Store, int?>(context, context.Stores), IStoreRepository
{ }
