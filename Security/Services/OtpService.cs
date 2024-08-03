using System.Security.Cryptography;
using System.Transactions;
using KanzApi.Account.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using KanzApi.Resources;
using KanzApi.Security.Entities;
using KanzApi.Security.Models;
using KanzApi.Security.Repositories;
using KanzApi.Utils;
using KanzApi.Vendors.Msegat.Services;
using Microsoft.Extensions.Localization;

namespace KanzApi.Security.Services;

public class OtpService(IOtpRepository repository,
ISessionService sessionService, IMsegatSmsService msegatSmsService,
IConfiguration configuration, IStringLocalizer<Messages> localizer)
: CrudService<Otp, int?>(repository), IOtpService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMsegatSmsService _msegatSmsService = msegatSmsService;

    private readonly IConfiguration _configuration = configuration;

    private readonly IStringLocalizer<Messages> _localizer = localizer;

    public Otp? FindByPrincipalId(int principalId)
    {
        return FindByPredicate(Otp.QPrincipalIdEquals(principalId));
    }

    public Otp GetByPrincipalId(int principalId)
    {
        return FindByPredicate(Otp.QPrincipalIdEquals(principalId))
        ?? throw EntityNotFoundException.From(typeof(Otp), "Principal ID", principalId);
    }

    public Otp GetByCurrentPrincipal()
    {
        int principalId = (int)_sessionService.CurrentPrincipalId()!;
        return GetByPrincipalId(principalId);
    }

    private string GenerateToken()
    {
        int result = RandomNumberGenerator.GetInt32(100000, 999999);
        return result.ToString();
    }

    public Otp Create(Principal principal)
    {
        Otp? entity = FindByPrincipalId((int)principal.Id!);
        if (entity != null) Remove(entity);

        int validity = _configuration.GetValue<int>("KanzApi:Otp:Validity");
        DateTime expiredAt = DateTime.Now.AddSeconds(validity);

        entity = new()
        {
            Code = GenerateToken(),
            Principal = principal,
            ExpiredAt = expiredAt,
            AttemptCount = 1
        };

        using TransactionScope scope = Transactions.Create();

        Add(entity);

        _msegatSmsService.Send(principal, _localizer.GetString("OtpSms", entity.Code));

        scope.Complete();

        return entity;
    }

    public Otp Recreate(Principal principal)
    {
        Otp entity = GetByPrincipalId((int)principal.Id!);
        int maxAttempt = _configuration.GetValue<int>("KanzApi:Otp:MaxAttempt");

        if (entity.AttemptCount >= maxAttempt)
        {
            Remove(entity);

            throw new OtpMaxAttemptException();
        }

        int validity = _configuration.GetValue<int>("KanzApi:Otp:Validity");
        DateTime expiredAt = DateTime.Now.AddSeconds(validity);

        entity.Code = GenerateToken();
        entity.ExpiredAt = expiredAt;
        entity.AttemptCount++;

        using TransactionScope scope = Transactions.Create();

        Edit(entity);

        _msegatSmsService.Send(principal, _localizer.GetString("OtpSms", entity.Code));

        scope.Complete();

        return entity;
    }

    public Otp Validate(OtpRequest request)
    {
        Otp entity = GetByCurrentPrincipal();
        if (entity.Code != request.Code)
        {
            throw new InvalidOtpException();
        }
        else if (entity.IsExpired())
        {
            throw new OtpExpiredException();
        }

        return entity;
    }
}
