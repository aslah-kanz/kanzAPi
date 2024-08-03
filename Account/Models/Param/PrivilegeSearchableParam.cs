using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class PrivilegeSearchableParam : SearchableParam<EPrivilegeSort, Privilege>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public PrivilegeSearchableParam() : base(EPrivilegeSort.UpdatedAt) { }

    protected override Expression<Func<Privilege, bool>> ToSearchPredicate(string search)
    {
        return Privilege.QNameContains(search);
    }
}
