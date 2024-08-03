using KanzApi.Account.Entities;
using KanzApi.Common.Services;
using KanzApi.Security.Entities;

namespace KanzApi.Security.Services;

public interface IRefreshTokenService : ICrudService<RefreshToken, int?>
{

    RefreshToken? FindByPrincipalId(int principalId);

    RefreshToken GetByPrincipalId(int principalId);

    RefreshToken Create(Principal principal, string accessTokenId);

    void Validate(RefreshToken entity, string? token, string accessTokenId);
}
