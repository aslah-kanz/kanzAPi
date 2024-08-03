using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class CustomerOrderProductResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public string? FamilyCode { get; set; }

    public ImageResponse? Icon { get; set; }

    public ImageResponse? Image { get; set; }

    public ProductBrandResponse Brand { get; set; } = new();

    public decimal Price { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public string? Comment { get; set; }
}
