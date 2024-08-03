using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewItem
{

    public Guid Id { get; set; }

    public ProductReviewPrincipalResponse Principal { get; set; } = new();

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public IEnumerable<ImageResponse> Images { get; set; } = [];

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
