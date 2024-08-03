using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalBankFilterableService : IFilterableCrudService<PrincipalBank, int?>
{

    PrincipalBankResponse Add(PrincipalBankRequest request);

    PrincipalBankResponse Edit(int id, PrincipalBankRequest request);

    PrincipalBankResponse RemoveModelById(int id);

    PrincipalBankResponse GetModelById(int id);

    PaginatedResponse<PrincipalBankResponse> FindAllModels(PrincipalBankPageableParam param);
}
