using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class CertificatePageableParam : PageableParam<ECertificateSort, Certificate>
{

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public CertificatePageableParam() : base(ECertificateSort.UpdatedAt) { }

    protected override Expression<Func<Certificate, bool>> ToSearchPredicate(string search)
    {
        return Certificate.QTitleContains(search);
    }
}
