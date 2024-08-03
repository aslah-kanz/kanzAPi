namespace KanzApi.Product.Models;

public class ProductPriceResponse
{
    public IList<long> ItemIds {get; set;} = [];

    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    public int Stock { get; set; }
}
