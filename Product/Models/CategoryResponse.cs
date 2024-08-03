using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class CategoryResponse
{

    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? Code { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string Slug { get; set; } = "";

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public bool ShowAtHomePage { get; set; }

    public ERecordStatus Status { get; set; }
}
