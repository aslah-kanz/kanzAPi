using KanzApi.Account.Entities;
using KanzApi.Account.Repositories;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public class StoreService(IStoreRepository repository)
: CrudService<Store, int?>(repository), IStoreService
{

    public Store IncreaseSaleItemCount(Store entity)
    {
        entity.SaleItemCount++;
        return Edit(entity);
    }

    public Store DecreaseSaleItemCount(Store entity)
    {
        entity.SaleItemCount--;
        return Edit(entity);
    }
}
