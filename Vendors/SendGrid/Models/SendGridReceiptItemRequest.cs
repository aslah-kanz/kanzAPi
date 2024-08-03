using KanzApi.Transaction.Entities;
using KanzApi.Utils;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridReceiptItemRequest
{

    public string? ImageUrl { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Quantity { get; set; }

    public string? Subtotal { get; set; }

    public static SendGridReceiptItemRequest From(CustomerOrderItem entity, string baseImageUrl)
    {
        return new()
        {
            ImageUrl = entity.Product!.Image!.ToUrl(baseImageUrl),
            Name = entity.Product.Name!.En,
            Description = entity.Product.Description?.En,
            Quantity = ((int)entity.Quantity!).ToString(Constants.IntegerFormat),
            Subtotal = "SAR " + ((decimal)entity.SubTotal!).ToString(Constants.DecimalFormat),
        };
    }
}
