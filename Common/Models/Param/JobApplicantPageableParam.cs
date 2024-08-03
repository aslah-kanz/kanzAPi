using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class JobApplicantPageableParam : PageableParam<EJobApplicantSort, JobApplicant>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public JobApplicantPageableParam() : base(EJobApplicantSort.UpdatedAt) { }

    protected override Expression<Func<JobApplicant, bool>> ToSearchPredicate(string search)
    {
        return JobApplicant.QNameContains(search);
    }
}
