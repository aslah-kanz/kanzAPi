using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class CompanyMemberPageableParam : PageableParam<EPrincipalSort, Principal>
{

    [SwaggerParameter("<i>Contains</i> : firstName, lastName")]
    public override string? Search { get; set; }

    public CompanyMemberPageableParam() : base(EPrincipalSort.UpdatedAt) { }

    protected override Expression<Func<Principal, bool>> ToSearchPredicate(string search)
    {
        return Principal.QFirstNameContains(search).Or(
            Principal.QLastNameContains(search));
    }
}
