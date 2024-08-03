using System.Text.Json.Serialization;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class BrandResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string Slug { get; set; } = "";

    public ImageResponse? Image { get; set; }

    public ImageResponse? BwImage { get; set; }

    public LocalizableString? Description { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public bool ShowAtHomePage { get; set; }

    public ERecordStatus Status { get; set; }

    [JsonPropertyName("categoryIds")]
    public ISet<int?> Categories { get; set; } = new HashSet<int?>();
}
