using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class WishListProductResponse
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public string? Code { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableNameResponse Brand { get; set; } = new();

    public decimal LowestPrice { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal HighestPrice { get; set; }
}
