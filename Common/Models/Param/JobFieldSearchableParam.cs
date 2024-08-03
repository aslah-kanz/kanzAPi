using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class JobFieldSearchableParam : SearchableParam<EJobFieldSort, JobField>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public JobFieldSearchableParam() : base(EJobFieldSort.UpdatedAt) { }

    protected override Expression<Func<JobField, bool>> ToSearchPredicate(string search)
    {
        return JobField.QNameContains(search);
    }
}
