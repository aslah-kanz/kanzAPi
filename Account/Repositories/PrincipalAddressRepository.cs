using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrincipalAddressRepository(CommonDbContext context)
: CrudRepository<PrincipalAddress, int?>(context, context.PrincipalAddresses), IPrincipalAddressRepository
{ }
