using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class PrincipalAddressPageableParam : PageableParam<EPrincipalAddressSort, PrincipalAddress>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public PrincipalAddressPageableParam() : base(EPrincipalAddressSort.UpdatedAt) { }

    protected override Expression<Func<PrincipalAddress, bool>> ToSearchPredicate(string search)
    {
        return PrincipalAddress.QNameEquals(search);
    }
}
