using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IRoleService : ICrudService<Role, int?>
{

    RoleResponse Add(RoleRequest request);

    RoleResponse Edit(int id, RoleRequest request);

    RoleResponse RemoveModelById(int id);

    RoleResponse GetModelById(int id);

    IEnumerable<RoleItem> FindAllModels(RoleSearchableParam param);
}
