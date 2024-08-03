using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class PurchaseQuotePageableParam : PageableParam<EPurchaseQuoteSort, PurchaseQuote>
{

    private EPurchaseQuoteStatus? _status;

    public EPurchaseQuoteStatus? Status { get { return _status; } set { _status = value; } }

    private int? _storeId;

    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    [SwaggerParameter("<i>Contains</i> : purchaseQuote, invoiceNumber")]
    public override string? Search { get; set; }

    public PurchaseQuotePageableParam() : base(EPurchaseQuoteSort.UpdatedAt) { }

    protected override Expression<Func<PurchaseQuote, bool>> ToSearchPredicate(string search)
    {
        return PurchaseQuote.QPurchaseQuoteCodeContains(search)
        .Or(PurchaseQuote.QInvoiceNumberContains(search));
    }

    public override Expression<Func<PurchaseQuote, bool>> ToPredicate()
    {
        Expression<Func<PurchaseQuote, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(PurchaseQuote.QStatusEquals((EPurchaseQuoteStatus)_status));
        }

        if (_storeId != null)
        {
            result = result.And(PurchaseQuote.QStoreIdEquals((int)_storeId));
        }

        return result;
    }
}
