using KanzApi.Transaction.Models;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Transaction.Services;

public interface ICustomerOrderPaymentService
{

    CustomerOrderPayResponse Pay(CustomerOrderPayRequest request);

    void PayCallback(UrwayWebHookRequest request);
}
