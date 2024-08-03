namespace KanzApi.Product.Models;

public class ProductPriceDetailResponse
{

    public int Id { get; set; }

    public double Price { get; set; }

    public double? OriginalPrice { get; set; }

    public int EstimatedDelivery { get; set; }

    public int Stock { get; set; }
}
