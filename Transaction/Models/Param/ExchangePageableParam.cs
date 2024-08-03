using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class ExchangePageableParam : PageableParam<EExchangeSort, Exchange>
{

    private EExchangeStatus? _status;

    public EExchangeStatus? Status { get { return _status; } set { _status = value; } }

    [SwaggerParameter("<i>Contains</i> : exchangeNumber")]
    public override string? Search { get; set; }

    public ExchangePageableParam() : base(EExchangeSort.UpdatedAt) { }

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

        return result;
    }
}
