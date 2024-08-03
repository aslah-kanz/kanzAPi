using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrivilegeService : ICrudService<Privilege, int?>
{

    PrivilegeResponse Add(PrivilegeRequest request);

    PrivilegeResponse Edit(int id, PrivilegeRequest request);

    PrivilegeResponse RemoveModelById(int id);

    PrivilegeResponse GetModelById(int id);

    IEnumerable<PrivilegeResponse> FindAllModels(PrivilegeSearchableParam param);
}
