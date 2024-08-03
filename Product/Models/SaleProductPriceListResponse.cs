using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class SaleProductPriceListResponse
{

    public int Id { get; set; }

    public string? Mpn { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string? FamilyCode { get; set; }

    public IEnumerable<ProductPriceResponse> Prices { get; set; } = [];
}
