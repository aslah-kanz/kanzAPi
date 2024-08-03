using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class InquiryPageableParam : PageableParam<EInquirySort, Inquiry>
{

    [SwaggerParameter("<i>Contains</i> : productName, slug, mpn")]
    public override string? Search { get; set; }

    public InquiryPageableParam() : base(EInquirySort.UpdatedAt) { }

    protected override Expression<Func<Inquiry, bool>> ToSearchPredicate(string search)
    {
        return Inquiry.QProductNameContains(search).Or(
            Inquiry.QProductSlugContains(search),
            Inquiry.QMpnContains(search));
    }
}
