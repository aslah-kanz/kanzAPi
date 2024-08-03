using System.Linq.Expressions;
using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Transaction.Models.Param;

public class ShippingMethodSearchableParam : SearchableParam<EShippingMethodSort, ShippingMethod>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public ShippingMethodSearchableParam() : base(EShippingMethodSort.Id, EOrder.Asc) { }

    protected override Expression<Func<ShippingMethod, bool>> ToSearchPredicate(string search)
    {
        return ShippingMethod.QNameContains(search);
    }
}
