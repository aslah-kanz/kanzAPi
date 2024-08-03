using System.Security.Cryptography;
using KanzApi.Account.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using KanzApi.Security.Entities;
using KanzApi.Security.Repositories;

namespace KanzApi.Security.Services;

public class RefreshTokenService(IRefreshTokenRepository repository,
ISessionService sessionService, IConfiguration configuration)
: CrudService<RefreshToken, int?>(repository), IRefreshTokenService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IConfiguration _configuration = configuration;

    public RefreshToken? FindByPrincipalId(int principalId)
    {
        return FindByPredicate(RefreshToken.QPrincipalIdEquals(principalId));
    }

    public RefreshToken GetByPrincipalId(int principalId)
    {
        return FindByPrincipalId(principalId)
        ?? throw EntityNotFoundException.From(typeof(RefreshToken), "Principal ID", principalId);
    }

    private string GenerateToken()
    {
        RandomNumberGenerator generator = RandomNumberGenerator.Create();

        byte[] randomNumber = new byte[32];
        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public RefreshToken Create(Principal principal, string accessTokenId)
    {
        RefreshToken? entity = FindByPrincipalId(principal.Id ?? default);
        if (entity != null)
        {
            Remove(entity);
        }

        int validity = _configuration.GetValue<int>("KanzApi:Jwt:RefreshTokenValidity");
        DateTime expiredAt = DateTime.Now.AddSeconds(validity);

        entity = new()
        {
            Token = GenerateToken(),
            AccessTokenId = accessTokenId,
            Principal = principal,
            ExpiredAt = expiredAt
        };
        return Add(entity);
    }

    public void Validate(RefreshToken entity, string? token, string accessTokenId)
    {
        if (!token?.Equals(entity.Token) ?? false || !accessTokenId.Equals(entity.AccessTokenId))
        {
            throw new InvalidTokenException();
        }
        else if (entity.IsExpired())
        {
            throw new TokenExpiredException();
        }
    }
}
