using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class OverviewCategoryItem
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }

    public bool ShowAtHomePage { get; set; }

    public ISet<OverviewProductResponse> Products { get; set; } = new HashSet<OverviewProductResponse>();
}
