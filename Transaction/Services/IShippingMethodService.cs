using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IShippingMethodService : ICrudService<ShippingMethod, int?>
{

    ShippingMethodResponse Add(ShippingMethodRequest request);

    ShippingMethodResponse Edit(int id, ShippingMethodRequest request);

    ShippingMethodResponse RemoveModelById(int id);

    ShippingMethodResponse GetModelById(int id);

    IEnumerable<ShippingMethodItem> FindAllModels(ShippingMethodSearchableParam param);
}
