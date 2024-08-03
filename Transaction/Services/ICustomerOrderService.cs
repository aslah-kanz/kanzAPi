using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Services;

public interface ICustomerOrderService : ICrudService<CustomerOrder, Guid?>
{

    void SetDeliveryOption(CustomerOrder entity, int? id);

    CustomerOrderCheckoutResponse ChangeDeliveryOption(int id);

    CustomerOrder? FindCurrent();

    CustomerOrder GetCurrent();

    CustomerOrder Create();

    CustomerOrder GetByPaymentTrackId(string paymentTrackId);

    CustomerOrderResponse GetModelById(Guid id);

    IEnumerable<CustomerOrderPurchaseQuoteResponse> GetPurchaseQuotesByCustomerOrderId(Guid id);

    StoreOrderGroupResponse GetModelByIdStoreOrderGroup(Guid id);
}
