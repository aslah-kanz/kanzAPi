using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public interface IPurchaseQuoteService : ICrudService<PurchaseQuote, Guid?>
{

    int UpdateAllStatuses(IEnumerable<PurchaseQuote> entities, EPurchaseQuoteStatus status);

    int UpdateAllAvailableStatusesByCustomerOrderId(Guid customerOrderId, EPurchaseQuoteStatus status);

    int RemoveAllByCustomerOrderId(Guid customerOrderId);

    PurchaseQuoteAwbResponse GetAwbByInvoiceNumber(string invoiceNumber);

    IEnumerable<PurchaseQuote> FindAllAvailablesByCustomerOrderId(Guid customerOrderId);

    IEnumerable<PurchaseQuote> FindAllByCustomerOrderId(Guid customerOrderId);

    IEnumerable<PurchaseQuote> FindAllAvailablesByStoreOrderId(Guid storeOrderId);

    List<Guid?> FindAllIds(Expression<Func<PurchaseQuote, bool>>? predicate);
}
