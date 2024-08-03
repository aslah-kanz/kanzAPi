using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ICatalogueService : ICrudService<Catalogue, int?>
{
    CatalogueResponse Add(CatalogueRequest request);

    CatalogueResponse Edit(int id, CatalogueRequest request);

    CatalogueResponse RemoveModelById(int id);

    CatalogueResponse GetModelById(int id);

    PaginatedResponse<CatalogueResponse> FindAllModels(CataloguePageableParam param);
}
