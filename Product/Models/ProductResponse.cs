using System.Text.Json.Serialization;
using KanzApi.Common.Models;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Models;

public class ProductResponse
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

    public double Length { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public decimal Price { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public bool Sellable { get; set; }

    public EProductStatus Status { get; set; }

    public string? Comment { get; set; }

    [JsonPropertyName("categoryIds")]
    public ISet<int?> Categories { get; set; } = new HashSet<int?>();

    public IEnumerable<string?> Documents { get; set; } = [];
}
