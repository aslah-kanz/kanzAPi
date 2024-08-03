using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class WebPageResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string Slug { get; set; } = "";

    public ImageResponse? Image { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public bool? ShowAtHomePage { get; set; }

    public EPublishableStatus? Status { get; set; }

    public IList<LocalizableString> Contents { get; set; } = [];
}
