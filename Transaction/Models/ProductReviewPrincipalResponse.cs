using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewPrincipalResponse
{

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public ImageResponse? Image { get; set; }
}
