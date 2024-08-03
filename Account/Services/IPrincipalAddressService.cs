using KanzApi.Account.Entities;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalAddressService : ICrudService<PrincipalAddress, int?>
{

    PrincipalAddress GetDefaultByPrincipalId(int principalId);
}
