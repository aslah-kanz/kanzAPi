using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrincipalBankRepository(CommonDbContext context)
: CrudRepository<PrincipalBank, int?>(context, context.PrincipalBanks), IPrincipalBankRepository
{ }
