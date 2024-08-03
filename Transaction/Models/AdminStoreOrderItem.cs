namespace KanzApi.Transaction.Models;

public class AdminStoreOrderItem
{
    public Guid Id { get; set; }
    public String Invoice { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal? GrandTotal { get; set; }
    public string Vendor { get; set; }
    public string Status { get; set; }
}
