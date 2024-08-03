namespace KanzApi.Transaction.Models;

public class AdminStoreOrderItemDetail : AdminStoreOrderItem
{
    public string Address { get; set; }
    public string CustomerName { get; set; }
    public List<AdminStoreOrderItemDetailProductList> ProductList { get; set; }

}

public class AdminStoreOrderItemDetailProductList
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
}
