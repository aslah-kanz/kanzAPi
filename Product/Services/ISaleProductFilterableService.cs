using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public interface ISaleProductFilterableService : IFilterableCrudService<ProductEntity, int?>
{

    IEnumerable<ProductPriceResponse> FindAllPricesById(int id, string? countryCode = null);

    SaleProductResponse Map(ProductEntity entity);

    PaginatedResponse<ProductFamilyProductItem> FindAllModelsByFamilyCode(SaleProductPageableParam param, string code);

    List<ProductMeilisearch> FindAllSearchablesById(SaleProductPageableParam param, int id);

    List<ProductMeilisearch> FindAllSearchablesByFamilyCode(SaleProductPageableParam param, string code);

    ProductFamilyPaginatedResponse FindAllFamilies(ProductFamilyPageableParam param);

    IEnumerable<string> FindAllRelatedFamiliesById(int id);

    IEnumerable<string> FindAllRelatedFamiliesBySlug(string slug);

    PaginatedResponse<ProductFamilyProductItem> FindAllModelsByFamilyCodePDP(SaleProductPageableParam param, string code);
}
