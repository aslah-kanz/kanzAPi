using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class PrincipalDetailPageableParam : PageableParam<EPrincipalDetailSort, PrincipalDetail>
{

    [SwaggerParameter("<i>Contains</i> : companyName")]
    public override string? Search { get; set; }

    public PrincipalDetailPageableParam() : base(EPrincipalDetailSort.UpdatedAt) { }

    protected override Expression<Func<PrincipalDetail, bool>> ToSearchPredicate(string search)
    {
        return PrincipalDetail.QCompanyNameEquals(search);
    }
}
