namespace KanzApi.Common.Models;

public class CertificateResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string? Slug { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ImageResponse? Image { get; set; }
}
