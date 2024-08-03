using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Services;

public interface ISaleItemSyncableService : ICrudService<SaleItem, long?>
{

    SaleItem Reserve(long id, int value, bool withoutStock = false);

    SaleItem Release(long id, int value);

    SaleItem Restore(long id, int value, bool resetStock = false);
}
