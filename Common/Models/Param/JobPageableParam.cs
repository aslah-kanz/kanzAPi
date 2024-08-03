using KanzApi.Common.Entities;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Common.Models.Param;

public class JobPageableParam : PageableParam<EJobSort, Job>
{
    private int? _jobFieldId;

    public int? JobFieldId { get { return _jobFieldId; } set { _jobFieldId = value; } }

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public JobPageableParam() : base(EJobSort.UpdatedAt) { }

    protected override Expression<Func<Job, bool>> ToSearchPredicate(string search)
    {
        return Job.QTitleContains(search);
    }

    public override Expression<Func<Job, bool>> ToPredicate()
    {
        Expression<Func<Job, bool>> result = base.ToPredicate();

        if (_jobFieldId != null)
        {
            result = result.And(Job.QJobFieldIdEquals((int)_jobFieldId));
        }

        return result;
    }
}
