using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Security.Models;

namespace KanzApi.Security.Services;

public class TokenService(IPrivilegeService privilegeService, IJwtTokenService jwtTokenService)
: ITokenService
{

    private readonly IPrivilegeService _privilegeService = privilegeService;

    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    public TokenResponse Generate(TokenRequest request)
    {
        List<Privilege> privileges = request.PrivilegeIds!.Select(id => _privilegeService.GetById(id)).ToList();
        return _jwtTokenService.Create(privileges, (int)request.Validity!);
    }
}
