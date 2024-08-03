using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class BannerResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string? Url { get; set; }

    public ImageResponse? Image { get; set; }

    public DateTime UpdatedAt { get; set; }

    public LocalizableString? Description { get; set; }

    public EPublishableStatus? Status { get; set; }
}
