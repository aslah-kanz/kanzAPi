using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class AdminExchangePageableParam : PageableParam<EExchangeSort, Exchange>
{

    private EExchangeStatus? _status;

    public EExchangeStatus? Status { get { return _status; } set { _status = value; } }

    private int? _storeId;

    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    [SwaggerParameter("<i>Contains</i> : ExchangeNumber")]
    public override string? Search { get; set; }

    public AdminExchangePageableParam() : base(EExchangeSort.UpdatedAt) { }

    protected override Expression<Func<Exchange, bool>> ToSearchPredicate(string search)
    {
        return Exchange.QNumberEquals(search);
    }

    public override Expression<Func<Exchange, bool>> ToPredicate()
    {
        Expression<Func<Exchange, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(Exchange.QStatusEquals((EExchangeStatus)_status));
        }
        if (_storeId != null)
        {
            result = result.And(Exchange.QStoreIdEquals((int)_storeId));
        }

        return result;
    }
}
