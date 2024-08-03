using KanzApi.Transaction.Entities;
using KanzApi.Utils;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridReceiptRequest : SendGridDataRequest
{

    public string? Title { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public List<string>? Addresses { get; set; }

    public List<SendGridReceiptItemRequest>? Items { get; set; }

    public string? Subtotal { get; set; }

    public List<SendGridReceiptAdditionalItemRequest>? AdditionalItems { get; set; }

    public string? Total { get; set; }

    public static SendGridReceiptRequest From(string title, CustomerOrder entity, string baseImageUrl)
    {
        return new()
        {
            Subject = title,
            Title = title,
            InvoiceNumber = entity.InvoiceNumber,
            Name = entity.Address!.RecipientName,
            PhoneNumber = entity.Address!.PhoneNumber,
            Addresses = [
                entity.Address!.Address,
                entity.Address!.City + ", " + entity.Address!.Country
            ],
            Items = entity.Items.Select(e => SendGridReceiptItemRequest.From(e, baseImageUrl)).ToList(),
            Subtotal = "SAR " + ((decimal)entity.SubTotal!).ToString(Constants.DecimalFormat),
            AdditionalItems = [
                SendGridReceiptAdditionalItemRequest.From("Discount", -entity.DiscountPrice ?? 0),
                SendGridReceiptAdditionalItemRequest.From("Delivery", (decimal)entity.EstimatedDeliveryCost!)
            ],
            Total = "SAR " + ((decimal)entity.GrandTotal!).ToString(Constants.DecimalFormat)
        };
    }
}
