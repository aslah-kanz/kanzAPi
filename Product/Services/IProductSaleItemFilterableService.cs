using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public interface IProductSaleItemFilterableService : IFilterableCrudService<ProductEntity, int?>
{

    PaginatedResponse<ProductItem> FindAllModels(ProductPageableParam param);
}
