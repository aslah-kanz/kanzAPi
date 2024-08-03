using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class CategoryItem
{

    public int Id { get; set; }

    public string? ParentId { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string Slug { get; set; } = "";

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }
}
