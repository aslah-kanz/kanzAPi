using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalDetailItemService : ICrudService<PrincipalDetailItem, int?>
{

    PrincipalDetailItem Add(PrincipalDetail principalDetail, PrincipalDetailItemRequest request);
}
