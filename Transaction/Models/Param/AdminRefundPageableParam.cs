using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class AdminRefundPageableParam : PageableParam<ERefundSort, Refund>
{

    private ERefundStatus? _status;

    public ERefundStatus? Status { get { return _status; } set { _status = value; } }

    private int? _storeId;

    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    [SwaggerParameter("<i>Contains</i> : refundNumber")]
    public override string? Search { get; set; }

    public AdminRefundPageableParam() : base(ERefundSort.UpdatedAt) { }

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
        if (_storeId != null)
        {
            result = result.And(Refund.QStoreIdEquals((int)_storeId));
        }

        return result;
    }
}
