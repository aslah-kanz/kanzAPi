using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class CustomerOrderPayRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("paymentMethodId")]
    public int? PaymentMethod { get; set; }

    public int? DeliveryOptionId { get; set; }

    public string? RedirectPath { get; set; }
}
