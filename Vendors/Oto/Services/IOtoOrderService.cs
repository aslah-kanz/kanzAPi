using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public interface IOtoOrderService
{

    OtoOrderResponse Create(OtoOrderRequest request);

    OtoOrderResponse Create(DeliveryOptionItem item, StoreOrder order, List<PurchaseQuote> purchaseQuotes);

    OtoOrderHistoryResponse FindAllHistories(OtoOrderHistoryRequest request);

    OtoOrderHistoryResponse FindAllHistories(params string[] invoiceNumbers);
}
