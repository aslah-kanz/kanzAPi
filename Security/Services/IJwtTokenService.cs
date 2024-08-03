using System.Security.Claims;
using KanzApi.Account.Entities;
using KanzApi.Security.Models;

namespace KanzApi.Security.Services;

public interface IJwtTokenService
{

    TokenResponse Create(List<Privilege> privileges, int validity);

    TokenResponse Create(Principal principal);

    TokenResponse CreateForOtp(Principal principal);

    TokenResponse CreateFromClaimsPrincipal(Principal principal, ClaimsPrincipal claimsPrincipal);

    ClaimsPrincipal ExtractPrincipal(string token);
}
