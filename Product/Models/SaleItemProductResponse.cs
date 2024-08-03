using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class SaleItemProductResponse
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

    public LocalizableString? Description { get; set; }

    public LocalizableNameResponse Brand { get; set; } = new();

    public ISet<CategoryResponse?> Categories { get; set; } = new HashSet<CategoryResponse?>();
}
