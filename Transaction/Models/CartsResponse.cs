namespace KanzApi.Transaction.Models;

public class CartsResponse
{
    public List<CartResponse>? Items { get; set; }
    public bool IsCartChanges { get; set; }
}