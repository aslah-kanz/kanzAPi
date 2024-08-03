using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrincipalDetailItemRepository(CommonDbContext context)
: CrudRepository<PrincipalDetailItem, int?>(context, context.PrincipalDetailItems), IPrincipalDetailItemRepository
{ }
