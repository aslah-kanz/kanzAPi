using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IFaqGroupService : ICrudService<FaqGroup, int?>
{
    FaqGroupResponse Add(FaqGroupRequest request);

    FaqGroupResponse Edit(int id, FaqGroupRequest request);

    FaqGroupResponse RemoveModelById(int id);

    FaqGroupResponse GetModelById(int id);

    PaginatedResponse<FaqGroupResponse> FindAllModels(FaqGroupPageableParam param);
}
