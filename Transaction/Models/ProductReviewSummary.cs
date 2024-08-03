using System.Text.Json.Serialization;
using KanzApi.Account.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewSummary
{

    [JsonIgnore]
    public int? StoreId { get; set; }

    public StoreResponse? Store { get; set; }

    [JsonIgnore]
    public int? ProductId { get; set; }

    public ProductReviewProductResponse? Product { get; set; }

    public int? TotalRating { get; set; }

    public int? ReviewerCount { get; set; }

    public double? RatingAverage { get; set; }
}
