using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Repositories;

namespace KanzApi.Transaction.Services;

public class CartService(ICartRepository repository)
: CrudService<Cart, int?>(repository), ICartService
{

    public override Cart Add(Cart entity)
    {
        throw new NotSupportedException();
    }

    public override Cart Edit(Cart entity)
    {
        throw new NotSupportedException();
    }

    public override Cart Remove(Cart entity)
    {
        throw new NotSupportedException();
    }

    public int Clear(int principalId)
    {
        return RemoveAllByPredicate(Cart.QPrincipalIdEquals(principalId));
    }
}
