using KanzApi.Security.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Security.Repositories;

public class RefreshTokenRepository(CommonDbContext context)
: CrudRepository<RefreshToken, int?>(context, context.RefreshTokens), IRefreshTokenRepository
{ }
