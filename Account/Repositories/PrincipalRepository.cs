using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrincipalRepository(CommonDbContext context)
: CrudRepository<Principal, int?>(context, context.Principals), IPrincipalRepository
{ }
