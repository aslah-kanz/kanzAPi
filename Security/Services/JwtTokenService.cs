using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanzApi.Account.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Extensions;
using KanzApi.Security.Models;
using KanzApi.Utils;
using Microsoft.IdentityModel.Tokens;

namespace KanzApi.Security.Services;

public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{

    private readonly IConfiguration _configuration = configuration;

    private readonly SecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiredAt) =>
        new(
            _configuration.GetValue<string>("KanzApi:Jwt:Issuer"),
            _configuration.GetValue<string>("KanzApi:Jwt:Audience"),
            claims,
            expires: expiredAt,
            signingCredentials: credentials
        );

    private List<Claim> CreateClaims(Principal? principal, string tokenId, DateTime issuedAt)
    {
        List<Claim> claims = [];

        if (principal != null)
        {
            claims.Add(new(JwtRegisteredClaimNames.Sub, principal.Id.ToString()!));
            claims.Add(new(JwtRegisteredClaimNames.Email, principal.Email!));
        }

        claims.Add(new(JwtRegisteredClaimNames.Jti, tokenId));

        long iat = new DateTimeOffset(issuedAt).ToUnixTimeSeconds();
        claims.Add(new(JwtRegisteredClaimNames.Iat, iat.ToString()));

        return claims;
    }

    private void ApplyPolicies(List<Claim> claims, Principal principal)
    {
        foreach (Role role in principal.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name!));
            foreach (Privilege privilege in role.Privileges)
            {
                claims.Add(new Claim(Constants.PolicyClaimType, privilege.Name!));
            }
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        string key = _configuration.GetValue<string>("KanzApi:Jwt:SigningKey")!;
        return new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);
    }

    public TokenResponse Create(List<Privilege> privileges, int validity)
    {
        string tokenId = Guid.NewGuid().ToString();
        DateTime issuedAt = DateTime.Now;
        DateTime expiredAt = issuedAt.AddSeconds(validity);

        List<Claim> claims = CreateClaims(null, tokenId, issuedAt);
        foreach (Privilege privilege in privileges)
        {
            claims.Add(new Claim(Constants.PolicyClaimType, privilege.Name!));
        }

        JwtSecurityToken token = CreateJwtToken(
            claims,
            CreateSigningCredentials(),
            expiredAt
        );

        return new TokenResponse()
        {
            Id = tokenId,
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = expiredAt
        };
    }

    public TokenResponse Create(Principal principal)
    {
        string tokenId = Guid.NewGuid().ToString();
        DateTime issuedAt = DateTime.Now;

        int validity = _configuration.GetValue<int>("KanzApi:Jwt:Validity");
        DateTime expiredAt = issuedAt.AddSeconds(validity);

        List<Claim> claims = CreateClaims(principal, tokenId, issuedAt);
        ApplyPolicies(claims, principal);

        JwtSecurityToken token = CreateJwtToken(
            claims,
            CreateSigningCredentials(),
            expiredAt
        );

        return new TokenResponse()
        {
            Id = tokenId,
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = expiredAt
        };
    }

    public TokenResponse CreateForOtp(Principal principal)
    {
        string tokenId = Guid.NewGuid().ToString();
        DateTime issuedAt = DateTime.Now;

        int validity = _configuration.GetValue<int>("KanzApi:Jwt:Validity");
        DateTime expiredAt = issuedAt.AddSeconds(validity);

        List<Claim> claims = CreateClaims(principal, tokenId, issuedAt);
        claims.Add(new Claim(Constants.PolicyClaimType, Privileges.RequestOtp));

        JwtSecurityToken token = CreateJwtToken(
            claims,
            CreateSigningCredentials(),
            expiredAt
        );

        return new TokenResponse()
        {
            Id = tokenId,
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = expiredAt
        };
    }

    public TokenResponse CreateFromClaimsPrincipal(Principal principal, ClaimsPrincipal claimsPrincipal)
    {
        return !claimsPrincipal.HasPolicy(Privileges.RequestOtp)
        ? Create(principal) : CreateForOtp(principal);
    }

    public ClaimsPrincipal ExtractPrincipal(string token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("KanzApi:Jwt:SigningKey")!))
        };

        try
        {
            return tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        }
        catch (Exception e)
        {
            throw new InvalidTokenException(e);
        }
    }
}
