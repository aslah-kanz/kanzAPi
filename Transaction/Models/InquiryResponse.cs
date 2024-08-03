using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class InquiryResponse
{

    public int Id { get; set; }

    public CartProductResponse Product { get; set; } = new();

    public decimal TotalPrice { get; set; }

    public int Quantity { get; set; }

    public string? Comment { get; set; }

    public EInquiryStatus? Status { get; set; }
}
