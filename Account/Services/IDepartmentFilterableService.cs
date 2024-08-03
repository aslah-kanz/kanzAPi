using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IDepartmentFilterableService : IFilterableCrudService<Department, int?>
{

    DepartmentResponse Add(DepartmentRequest request);

    DepartmentResponse Edit(int id, DepartmentRequest request);

    DepartmentResponse RemoveModelById(int id);

    DepartmentResponse GetModelById(int id);

    PaginatedResponse<DepartmentResponse> FindAllModels(DepartmentPageableParam param);
}
