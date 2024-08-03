using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class CustomerOrderChangeAddressRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("addressId")]
    public int? Address { get; set; }
}
