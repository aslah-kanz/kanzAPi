using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IFaqService : ICrudService<Faq, int?>
{

    FaqResponse Add(FaqRequest request);

    FaqResponse Edit(int id, FaqRequest request);

    FaqResponse RemoveModelById(int id);

    FaqResponse GetModelById(int id);

    PaginatedResponse<FaqResponse> FindAllModels(FaqPageableParam param);
}
