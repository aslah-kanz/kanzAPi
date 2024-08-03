namespace KanzApi.Transaction.Models;

public class CartResponse
{

    public int Id { get; set; }

    public CartProductResponse? Product { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int Stock { get; set; }

    public string? Comment { get; set; }

}
