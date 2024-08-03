using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class PurchaseQuoteInvoicePageableParam : PageableParam<EPurchaseQuoteInvoiceSort, PurchaseQuote>
{

    private EPurchaseQuoteStatus? _status;

    public EPurchaseQuoteStatus? Status { get { return _status; } set { _status = value; } }

    [SwaggerParameter("<i>Contains</i> : invoiceNumber")]
    public override string? Search { get; set; }

    public PurchaseQuoteInvoicePageableParam() : base(EPurchaseQuoteInvoiceSort.CreatedAt) { }

    protected override Expression<Func<PurchaseQuote, bool>> ToSearchPredicate(string search)
    {
        return PurchaseQuote.QInvoiceNumberContains(search);
    }

    public override Expression<Func<PurchaseQuote, bool>> ToPredicate()
    {
        Expression<Func<PurchaseQuote, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(PurchaseQuote.QStatusEquals((EPurchaseQuoteStatus)_status));
        }

        return result;
    }
}
