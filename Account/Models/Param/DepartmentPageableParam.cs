using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class DepartmentPageableParam : PageableParam<EDepartmentSort, Department>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public DepartmentPageableParam() : base(EDepartmentSort.UpdatedAt) { }

    protected override Expression<Func<Department, bool>> ToSearchPredicate(string search)
    {
        return Department.QNameEquals(search);
    }
}
