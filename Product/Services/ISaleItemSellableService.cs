using KanzApi.Common.Services;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Services;

public interface ISaleItemSellableService : ICrudService<SaleItem, long?>
{

    IEnumerable<SaleItem> FindAllProducts(int id);

    IEnumerable<SaleItem> FindAllByProductId(int id);

    IEnumerable<SaleItem> FindAllByProductIdAndCountry(int id, string countryCode);
}
