using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Security.Entities;

namespace KanzApi.Security.Services;

public interface IOneTimeTokenService : ICrudService<OneTimeToken, int?>
{

    OneTimeToken GetByTokenAndType(string token, EOneTimeTokenType type);

    OneTimeToken? FindByPrincipalIdAndType(int principalId, EOneTimeTokenType type);

    OneTimeToken GetByPrincipalIdAndType(int principalId, EOneTimeTokenType type);

    OneTimeToken Create(Principal principal, EOneTimeTokenType type);

    OneTimeToken Validate(string token, EOneTimeTokenType type);
}
