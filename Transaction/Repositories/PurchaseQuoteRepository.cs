using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using System.Linq.Expressions;
using KanzApi.Extensions;

namespace KanzApi.Transaction.Repositories;

public class PurchaseQuoteRepository(CommonDbContext context)
: CrudRepository<PurchaseQuote, Guid?>(context, context.PurchaseQuotes), IPurchaseQuoteRepository
{
    public List<Guid?> FindAllIds(Expression<Func<PurchaseQuote, bool>>? predicate)
    {
        IQueryable<PurchaseQuote> query = _set;
        if (predicate != null) query = query.Where(predicate);
        return query.Select(e => e.Id).ToList();
    }

    public PaginatedEntity<PurchaseQuoteInvoiceItem> FindAllInvoices(Page page,
    Expression<Func<PurchaseQuote, bool>>? predicate, Sort? sort)
    {
        IQueryable<PurchaseQuote> query = _set;
        if (predicate != null) query = query.Where(predicate);

        IQueryable<PurchaseQuoteInvoiceItem> querySummary = query
        .GroupBy(e => new { e.StoreOrderId })
        .Select(g => new PurchaseQuoteInvoiceItem()
        {
            InvoiceNumber = g.Select(e => e.StoreOrder!.InvoiceNumber).First(),
            ProductCount = g.Count(),
            StoreName = g.Select(e => e.Store!.Name).First(),
            Status = g.Select(e => e.StoreOrder!.PurchaseQuoteStatus).First(),
            Profit = g.Select(e => e.SubTotal).Sum(),
            Total = g.Select(e => e.SubTotal.Value).Sum() + g.Select(e => e.Tax.Value).Sum() + g.Select(e => e.PlatformCommission.Value).Sum(),
            CreatedAt = g.Min(e => e.CreatedAt)
        });

        int count = querySummary.Count();

        if (sort != null) querySummary = querySummary.Sort(sort);

        IEnumerable<PurchaseQuoteInvoiceItem> entities = [.. querySummary
        .Skip(page.Index * page.Size)
        .Take(page.Size)];

        return new PaginatedEntity<PurchaseQuoteInvoiceItem>(entities, page.Index, page.Size, count);
    }
}
