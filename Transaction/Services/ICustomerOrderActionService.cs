using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Services;

public interface ICustomerOrderActionService
{

    CustomerOrderCheckoutResponse ChangeAddress(int addressId);

    CustomerOrderCheckoutResponse Checkout();

	CustomerOrderCheckoutResponse BuyNow(CustomerOrderBuyNowRequest request);

	void Cancel(Guid id);
}
