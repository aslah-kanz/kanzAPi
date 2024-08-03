using System.Linq.Expressions;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Repositories;

public interface IPurchaseQuoteRepository : ICrudRepository<PurchaseQuote, Guid?>
{

    PaginatedEntity<PurchaseQuoteInvoiceItem> FindAllInvoices(Page page,
    Expression<Func<PurchaseQuote, bool>>? predicate, Sort? sort);

    List<Guid?> FindAllIds(Expression<Func<PurchaseQuote, bool>>? predicate);
}
