using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IBannerService : ICrudService<Banner, int?>
{
    BannerResponse Add(BannerRequest request);

    BannerResponse Edit(int id, BannerRequest request);

    BannerResponse RemoveModelById(int id);

    BannerResponse GetModelById(int id);

    PaginatedResponse<BannerResponse> FindAllModels(BannerPageableParam param);
}
