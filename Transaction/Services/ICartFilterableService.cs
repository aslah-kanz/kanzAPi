using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Services;

public interface ICartFilterableService : IFilterableCrudService<Cart, int?>
{

    CartResponse Add(AddCartRequest request);

    CartResponse Edit(int id, EditCartRequest request);

    CartResponse RemoveModelById(int id);

    int RemoveAllByProductIds(ISet<int> productIds);

    CartResponse GetModelById(int id);

    Cart? FindByProductIdAndPrice(int productId, decimal price);

    CartsResponse FindAllModels();
}
