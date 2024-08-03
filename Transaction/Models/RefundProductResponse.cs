using KanzApi.Common.Models;
using KanzApi.Product.Models;

namespace KanzApi.Transaction.Models;

public class RefundProductResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public string? FamilyCode { get; set; }

    public ImageResponse? Image { get; set; }

    public ProductBrandResponse Brand { get; set; } = new();
}
