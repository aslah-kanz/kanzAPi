using KanzApi.Account.Entities;
using KanzApi.Common.Services;
using KanzApi.Security.Entities;
using KanzApi.Security.Models;

namespace KanzApi.Security.Services;

public interface IOtpService : ICrudService<Otp, int?>
{

    Otp? FindByPrincipalId(int principalId);

    Otp GetByPrincipalId(int principalId);

    Otp GetByCurrentPrincipal();

    Otp Create(Principal principal);

    Otp Recreate(Principal principal);

    Otp Validate(OtpRequest request);
}
