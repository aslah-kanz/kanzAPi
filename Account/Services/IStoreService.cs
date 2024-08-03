using KanzApi.Account.Entities;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IStoreService : ICrudService<Store, int?>
{

    Store IncreaseSaleItemCount(Store entity);

    Store DecreaseSaleItemCount(Store entity);
}
