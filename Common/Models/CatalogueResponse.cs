using System.Text.Json.Serialization;

namespace KanzApi.Common.Models;

public class CatalogueResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string? Slug { get; set; }

    public ImageResponse? Image { get; set; }

    public DocumentResponse? Document { get; set; }

    public LocalizableString? Description { get; set; }

    [JsonPropertyName("date")]
    public DateTime UpdatedAt { get; set; }

    public int ReadCount { get; set; }
}
