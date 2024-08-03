using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class ProductFamilyProductItem
{

    public int Id { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public ImageResponse? Icon { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableNameResponse Brand { get; set; } = new();

    public double LowestPrice { get; set; }

    public double? OriginalPrice { get; set; }

    public double? HighestPrice { get; set; }

    public bool IsAvailable { get; set; }

}
