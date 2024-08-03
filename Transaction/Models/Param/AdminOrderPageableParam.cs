using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class AdminOrderPageableParam : PageableParam<ECustomerOrderSort, CustomerOrder>
{

    private ECustomerOrderStatus? _status;

    public ECustomerOrderStatus? Status { get { return _status; } set { _status = value; } }

    [SwaggerParameter("<i>Contains</i> : invoiceNumber")]
    public override string? Search { get; set; }

    public AdminOrderPageableParam() : base(ECustomerOrderSort.UpdatedAt) { }

    protected override Expression<Func<CustomerOrder, bool>> ToSearchPredicate(string search)
    {
        return CustomerOrder.QInvoiceNumberContains(search);
    }

    public override Expression<Func<CustomerOrder, bool>> ToPredicate()
    {
        Expression<Func<CustomerOrder, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(CustomerOrder.QStatusEquals((ECustomerOrderStatus)_status));
        }

        return result;
    }
}
