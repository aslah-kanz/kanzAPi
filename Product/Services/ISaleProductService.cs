using KanzApi.Common.Services;
using KanzApi.Product.Models;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public interface ISaleProductService : ICrudService<ProductEntity, int?>
{

    SaleProductResponse GetModelById(int id);

    SaleProductPriceListResponse GetProductPriceListById(int id);

    SaleProductPriceListResponse GetProductPriceListByIdAndCountryCode(int id, string countryCode);

    SaleProductResponse GetModelBySlug(string slug);
}
