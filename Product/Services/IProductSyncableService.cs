using KanzApi.Common.Services;
using KanzApi.Product.Models;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public interface IProductSyncableService : ICrudService<ProductEntity, int?>
{
    ProductResponse Add(ProductRequest request);

    ProductResponse ChangeStatus(int id, ProductStatusRequest request);

    ProductResponse Edit(int id, ProductRequest request);

    ProductResponse RemoveModelById(int id);

    ProductResponse GetModelById(int id);

    IEnumerable<OverviewCategoryItem> FindAllOverviewCategoryModels();

}
