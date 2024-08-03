using System.Text.Json.Serialization;

namespace KanzApi.Common.Models;

public class WebsiteProfileResponse
{

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public string? Instagram { get; set; }

    public string? Twitter { get; set; }

    public string? Facebook { get; set; }

    public string? Linkedin { get; set; }

    public string? Youtube { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    [JsonPropertyName("logo")]
    public ImageResponse? Image { get; set; }

    public ImageResponse? Favicon { get; set; }
}
