using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrincipalDetailRepository(CommonDbContext context)
: CrudRepository<PrincipalDetail, int?>(context, context.PrincipalDetails), IPrincipalDetailRepository
{ }
