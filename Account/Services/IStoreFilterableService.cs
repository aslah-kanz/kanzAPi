using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IStoreFilterableService : IFilterableCrudService<Store, int?>
{

    StoreResponse Add(StoreRequest request);

    StoreResponse Edit(int id, StoreRequest request);

    StoreResponse RemoveModelById(int id);

    Store Inactivate(Store entity);

    Store Activate(Store entity);

    StoreResponse GetModelById(int id);

    PaginatedResponse<StoreResponse> FindAllModels(StorePageableParam param);

    IEnumerable<NameableResponse> FindAllModels(StoreSearchableParam param);
}
