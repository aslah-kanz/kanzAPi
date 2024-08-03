using KanzApi.Security.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Security.Repositories;

public class OneTimeTokenRepository(CommonDbContext context)
: CrudRepository<OneTimeToken, int?>(context, context.OneTimeTokens), IOneTimeTokenRepository
{ }
