using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IPurchaseQuoteFilterableService : IFilterableCrudService<PurchaseQuote, Guid?>
{

    PurchaseQuoteResponse GetModelById(Guid id);

    IEnumerable<PurchaseQuote> FindAllByInvoiceNumber(string invoiceNumber);

    PurchaseQuoteInvoiceResponse GetInvoiceByInvoiceNumber(string invoiceNumber);

    PaginatedResponse<PurchaseQuoteInvoiceItem> FindAllInvoices(PurchaseQuoteInvoicePageableParam param);

    PaginatedResponse<PurchaseQuoteResponse> FindAllModels(PurchaseQuotePageableParam param);
}
