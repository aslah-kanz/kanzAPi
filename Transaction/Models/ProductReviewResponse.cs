using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewResponse
{

    public Guid Id { get; set; }

    public Guid PurchaseQuoteId { get; set; }

    public string? Slug { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public List<ImageResponse> Images { get; set; } = [];
}
