using KanzApi.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KanzApi.Extensions;

public static class ClaimsPrincipals
{

    public static string? ClaimValueByType(this ClaimsPrincipal source, string type)
    {
        return source.Claims.FirstOrDefault(c => c.Type.Equals(type))?.Value;
    }

    public static int? ClaimIntValueByType(this ClaimsPrincipal source, string type)
    {
        string? value = source.ClaimValueByType(type);
        return value != null ? Int32.Parse(value) : null;
    }

    public static int? PrincipalId(this ClaimsPrincipal source)
    {
        return source.ClaimIntValueByType(ClaimTypes.NameIdentifier);
    }

    public static string? TokenId(this ClaimsPrincipal source)
    {
        return source.ClaimValueByType(JwtRegisteredClaimNames.Jti);
    }

    public static bool HasPolicy(this ClaimsPrincipal source, string name)
    {
        return source.Claims.FirstOrDefault(c
        => c.Type.Equals(Constants.PolicyClaimType) && c.Value.Equals(name)) != null;
    }
}
