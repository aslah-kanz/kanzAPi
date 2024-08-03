using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class RefundPageableParam : PageableParam<ERefundSort, Refund>
{

    private ERefundStatus? _status;

    public ERefundStatus? Status { get { return _status; } set { _status = value; } }

    [SwaggerParameter("<i>Contains</i> : refundNumber")]
    public override string? Search { get; set; }

    public RefundPageableParam() : base(ERefundSort.UpdatedAt) { }

    protected override Expression<Func<Refund, bool>> ToSearchPredicate(string search)
    {
        return Refund.QNumberEquals(search);
    }

    public override Expression<Func<Refund, bool>> ToPredicate()
    {
        Expression<Func<Refund, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(Refund.QStatusEquals((ERefundStatus)_status));
        }

        return result;
    }
}
