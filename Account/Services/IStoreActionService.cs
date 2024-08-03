using KanzApi.Account.Models;

namespace KanzApi.Account.Services;

public interface IStoreActionService
{

    StoreResponse Activate(int id);

    StoreResponse Inactivate(int id);
}
