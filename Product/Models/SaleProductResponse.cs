using KanzApi.Common.Models;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Models;

public class SaleProductResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? Slug { get; set; }

    public string? FamilyCode { get; set; }

    public ImageResponse? Icon { get; set; }

    public ImageResponse? Image { get; set; }

    public LocalizableString? Description { get; set; }

    public ProductBrandResponse Brand { get; set; } = new();

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public EProductStatus Status { get; set; }

    public string? Comment { get; set; }

    [Obsolete]
    public decimal? Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    [Obsolete]
    public decimal? MaxPrice { get; set; }

    public decimal? LowestPrice { get; set; }

    public decimal? HighestPrice { get; set; }

    public IEnumerable<ProductPriceResponse> Prices { get; set; } = [];

    public IEnumerable<ImageResponse> Images { get; set; } = [];

    public IEnumerable<DocumentResponse> Documents { get; set; } = [];

    [Obsolete("Should call /product-families/{code}/products")]
    public IEnumerable<ProductFamilyProductItem> OtherProducts { get; set; } = [];

    public IEnumerable<SalePropertyResponse> Properties { get; set; } = [];

    public IEnumerable<ProductCategoryResponse> CategoryTree { get; set; } = [];
}
