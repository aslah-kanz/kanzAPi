using System.Linq.Expressions;
using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Transaction.Models.Param;

public class PaymentMethodSearchableParam : SearchableParam<EPaymentMethodSort, PaymentMethod>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public PaymentMethodSearchableParam() : base(EPaymentMethodSort.Id, EOrder.Asc) { }

    protected override Expression<Func<PaymentMethod, bool>> ToSearchPredicate(string search)
    {
        return PaymentMethod.QNameContains(search);
    }
}
