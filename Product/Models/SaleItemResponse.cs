using KanzApi.Account.Models;
using KanzApi.Common.Entities;

namespace KanzApi.Product.Models;

public class SaleItemResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public SaleItemProductResponse? Product { get; set; }

    public string? VendorSku { get; set; }

    public int? Stock { get; set; } = 0;

    public decimal? MinPrice { get; set; } = 0;

    public decimal? MaxPrice { get; set; } = 0;

    public decimal? DiscountPrice { get; set; } = 0;

    public int? MinOrderQuantity { get; set; } = 0;

    public int? MaxOrderQuantity { get; set; } = 0;

    public StoreResponse? Store { get; set; }

    public bool? Enabled { get; set; } = true;

    public EActivableStatus? Status { get; set; }
}
