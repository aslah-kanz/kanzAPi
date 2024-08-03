using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;

namespace KanzApi.Product.Services;

public interface ISaleItemFilterableService : IFilterableCrudService<SaleItem, long?>
{

    SaleItemResponse Add(SaleItemRequest request);

    SaleItemResponse ChangeEnabled(long id, bool enabled);

    SaleItemResponse Edit(long id, SaleItemRequest request);

    SaleItemResponse RemoveModelById(long id);

    SaleItemResponse Activate(long id);

    SaleItemResponse Inactivate(long id);

    SaleItemResponse GetModelById(long id);

    IEnumerable<SaleItem> FindAllByProductId(int id);

    IEnumerable<SaleItem> FindAllByStoreId(int id);

    PaginatedResponse<SaleItemResponse> FindAllModels(SaleItemPageableParam param);

    PaginatedResponse<SaleItemStoreResponse> FindAllModels(int productId, SaleItemPageableParam param);
}
