using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Services;

public interface ICartService : ICrudService<Cart, int?>
{

    int Clear(int principalId);
}
