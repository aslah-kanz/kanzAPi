using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalAddressFilterableService : IFilterableCrudService<PrincipalAddress, int?>
{

    PrincipalAddressResponse Add(PrincipalAddressRequest request);

    PrincipalAddressResponse Edit(int id, PrincipalAddressRequest request);

    PrincipalAddressResponse ChangeDefault(int id);

    PrincipalAddressResponse RemoveModelById(int id);

    PrincipalAddressResponse GetModelById(int id);

    PrincipalAddress GetDefault();

    PaginatedResponse<PrincipalAddressItem> FindAllModels(PrincipalAddressPageableParam param);
}
