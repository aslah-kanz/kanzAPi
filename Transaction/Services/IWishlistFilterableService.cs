using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IWishListFilterableService : IFilterableCrudService<WishList, int?>
{

    WishListResponse Add(WishListRequest request);

    WishListResponse Edit(int id, WishListRequest request);

    WishListResponse RemoveModelById(int id);

    WishListResponse RemoveModelByProductId(int productId);

    WishListResponse GetModelById(int id);

    WishList GetByProductId(int productId);

    PaginatedResponse<WishListResponse> FindAllModels(WishListPageableParam param);

    IEnumerable<int> FindAllProductIds();
}
