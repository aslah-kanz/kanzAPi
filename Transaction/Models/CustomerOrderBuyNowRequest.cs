namespace KanzApi.Transaction.Models;

public class CustomerOrderBuyNowRequest
{

    public int ProductId { get; set; }

	public List<CustomerOrderBuyNowSaleItem> SaleItems { get; set; } = [];
}

public class CustomerOrderBuyNowSaleItem
{

	public int SaleItemId { get; set; }

	public int Quantity { get; set; }

	public decimal Price { get; set; }

	public int Stock { get; set; }

	public string? Comment { get; set; }

}