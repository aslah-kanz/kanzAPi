using KanzApi.Common.Entities;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Repositories;

namespace KanzApi.Product.Services;

public class SaleItemSellableService(ISaleItemRepository repository)
: CrudService<SaleItem, long?>(repository), ISaleItemSellableService
{

    public override SaleItem Add(SaleItem entity)
    {
        throw new NotSupportedException();
    }

    public override SaleItem Edit(SaleItem entity)
    {
        throw new NotSupportedException();
    }

    public override SaleItem Remove(SaleItem entity)
    {
        throw new NotSupportedException();
    }

    public IEnumerable<SaleItem> FindAllProducts(int id)
    {
        return FindAll(SaleItem.QPrincipalIdEquals(id)
       .And(SaleItem.QEnabledEquals(true), SaleItem.QStockNotEmpty(), SaleItem.QStatusEquals(EActivableStatus.Active)), null);
    }

    public IEnumerable<SaleItem> FindAllByProductId(int id)
    {
        return FindAll(SaleItem.QProductIdEquals(id)
        .And(SaleItem.QEnabledEquals(true), SaleItem.QStockNotEmpty(),
        SaleItem.QStatusEquals(EActivableStatus.Active)), null);
    }

    public IEnumerable<SaleItem> FindAllByProductIdAndCountry(int id, string countryCode)
    {
        return FindAll(SaleItem.QProductIdEquals(id)
        .And(SaleItem.QEnabledEquals(true), SaleItem.QStockNotEmpty(),
        SaleItem.QStatusEquals(EActivableStatus.Active),
        SaleItem.QCountryEquals(countryCode)), null);
    }
}
