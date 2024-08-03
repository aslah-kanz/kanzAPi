using KanzApi.Common.Services;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Services;

public interface IProductImageService : ICrudService<ProductImage, int?>
{

    ProductImageResponse Add(int productId, ProductImageRequest request);

    ProductImageResponse Edit(int productId, int id, ProductImageRequest request);

    ProductImageResponse RemoveModelById(int productId, int id);

    ProductImageResponse GetModelById(int productId, int id);

    IEnumerable<ProductImageResponse> FindAllModels(int productId, ProductImageSortableParam param);
}
