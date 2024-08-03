namespace KanzApi.Common.Models;

public class BlogResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string? Slug { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public DateTime UpdatedAt { get; set; }
}
