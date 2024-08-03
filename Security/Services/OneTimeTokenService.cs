using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Security.Entities;
using KanzApi.Security.Repositories;
using System.Security.Cryptography;

namespace KanzApi.Security.Services;

public class OneTimeTokenService(IOneTimeTokenRepository repository, IConfiguration configuration)
: CrudService<OneTimeToken, int?>(repository), IOneTimeTokenService
{

    private readonly IConfiguration _configuration = configuration;

    public OneTimeToken RemoveByTokenAndType(string token, EOneTimeTokenType type)
    {
        OneTimeToken entity = GetByTokenAndType(token, type);
        return Remove(entity);
    }

    public OneTimeToken GetByTokenAndType(string token, EOneTimeTokenType type)
    {
        return FindByPredicate(OneTimeToken.QTokenEquals(token)
        .And(OneTimeToken.QTypeEquals(type)))
        ?? throw EntityNotFoundException.From(typeof(OneTimeToken), "Token", token);
    }

    public OneTimeToken? FindByPrincipalIdAndType(int principalId, EOneTimeTokenType type)
    {
        return FindByPredicate(OneTimeToken.QPrincipalIdEquals(principalId)
        .And(OneTimeToken.QTypeEquals(type)));
    }

    public OneTimeToken GetByPrincipalIdAndType(int principalId, EOneTimeTokenType type)
    {
        return FindByPrincipalIdAndType(principalId, type)
        ?? throw EntityNotFoundException.From(typeof(OneTimeToken),
        new Dictionary<string, object?> { { "Principal ID", principalId }, { "Type", type } });
    }

    private string GenerateToken()
    {
        RandomNumberGenerator generator = RandomNumberGenerator.Create();

        byte[] randomNumber = new byte[64];
        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public OneTimeToken Create(Principal principal, EOneTimeTokenType type)
    {
        OneTimeToken? entity = FindByPrincipalIdAndType(principal.Id ?? default, type);
        if (entity != null)
        {
            Remove(entity);
        }

        int validity = _configuration.GetValue<int>("KanzApi:Activation:Validity");
        DateTime expiredAt = DateTime.Now.AddSeconds(validity);

        entity = new()
        {
            Type = type,
            Token = GenerateToken(),
            Principal = principal,
            ExpiredAt = expiredAt
        };

        entity = Add(entity);

        return entity;
    }

    public OneTimeToken Validate(string token, EOneTimeTokenType type)
    {
        OneTimeToken entity = GetByTokenAndType(token, type);
        if (!token.Equals(entity.Token))
        {
            throw new InvalidTokenException();
        }
        else if (entity.IsExpired())
        {
            throw new TokenExpiredException();
        }

        return entity;
    }
}
