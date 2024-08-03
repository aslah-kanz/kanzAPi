using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class PrincipalBankPageableParam : PageableParam<EPrincipalBankSort, PrincipalBank>
{

    [SwaggerParameter("<i>Contains</i> : bankName, beneficiaryName, iban")]
    public override string? Search { get; set; }

    public PrincipalBankPageableParam() : base(EPrincipalBankSort.UpdatedAt) { }

    protected override Expression<Func<PrincipalBank, bool>> ToSearchPredicate(string search)
    {
        return PrincipalBank.QNameEquals(search).Or(
            PrincipalBank.QBeneficiaryNameContains(search),
            PrincipalBank.QIbanEquals(search));
    }
}
