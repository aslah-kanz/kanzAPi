using KanzApi.Common.Entities;

namespace KanzApi.Product.Models;

public class SaleItemMeilisearch
{

    public long Id { get; set; }

    public string? Code { get; set; }

    public string? VendorSku { get; set; }

    public int? Stock { get; set; } = 0;

    public decimal? MinPrice { get; set; } = 0;

    public decimal? MaxPrice { get; set; } = 0;

    public decimal? DiscountPrice { get; set; } = 0;

    public decimal? Price { get; set; } = 0;

    public decimal? OriginalPrice { get; set; } = 0;

    public int? MinOrderQuantity { get; set; } = 0;

    public int? MaxOrderQuantity { get; set; } = 0;

    public string? Country { get; set; }

    public Dictionary<string, string>? Symbol { get; set; }

    public bool? Enabled { get; set; } = true;

    public EActivableStatus? Status { get; set; }
}
