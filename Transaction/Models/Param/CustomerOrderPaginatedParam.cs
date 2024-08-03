using System.Linq.Expressions;
using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Transaction.Models.Param;

public class CustomerOrderPageableParam : PageableParam<ECustomerOrderSort, CustomerOrder>
{

    [SwaggerParameter("<i>Contains</i> : invoiceNumber")]
    public override string? Search { get; set; }

    public CustomerOrderPageableParam() : base(ECustomerOrderSort.UpdatedAt) { }

    protected override Expression<Func<CustomerOrder, bool>> ToSearchPredicate(string search)
    {
        return CustomerOrder.QInvoiceNumberContains(search);
    }
}
