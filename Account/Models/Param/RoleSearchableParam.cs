using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class RoleSearchableParam : SearchableParam<ERoleSort, Role>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public RoleSearchableParam() : base(ERoleSort.UpdatedAt) { }

    protected override Expression<Func<Role, bool>> ToSearchPredicate(string search)
    {
        return Role.QNameContains(search);
    }
}
