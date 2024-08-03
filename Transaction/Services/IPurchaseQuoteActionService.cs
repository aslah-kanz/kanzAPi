using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Services;

public interface IPurchaseQuoteActionService
{

    void OpenAllByCustomerOrderId(Guid customerOrderId);

    PurchaseQuoteResponse Accept(Guid id, PurchaseQuoteAcceptRequest request);

    PurchaseQuoteResponse Reject(Guid id, PurchaseQuoteRejectRequest request);

    void RequestPickup(string invoiceNumber, int packageCount);

    void UpdateAllAvailableStatusesByInvoiceNumber(string invoiceNumber, EPurchaseQuoteStatus status);
}
