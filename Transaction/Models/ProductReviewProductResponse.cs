using System.Text.Json.Serialization;
using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewProductResponse
{

    public int Id { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public string? FamilyCode { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }

    public LocalizableNameResponse Brand { get; set; } = new();

    [JsonPropertyName("categoryIds")]
    public ISet<int?> Categories { get; set; } = new HashSet<int?>();
}
