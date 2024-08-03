using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IPaymentMethodService : ICrudService<PaymentMethod, int?>
{

    PaymentMethodResponse Add(PaymentMethodRequest request);

    PaymentMethodResponse Edit(int id, PaymentMethodRequest request);

    PaymentMethodResponse RemoveModelById(int id);

    PaymentMethodResponse GetModelById(int id);

    IEnumerable<PaymentMethodItem> FindAllModels(PaymentMethodSearchableParam param);
}
