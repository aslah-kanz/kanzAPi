using KanzApi.Common.Models;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Models;

public class ProductItem
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Mpn { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? FamilyCode { get; set; }

    public ImageResponse? Icon { get; set; }

    public ImageResponse? Image { get; set; }

    public string? Description { get; set; }

    public ProductBrandResponse? Brand { get; set; }

    public EProductStatus Status { get; set; }

    public List<long> StoreIds { get; set; } = [];
}
