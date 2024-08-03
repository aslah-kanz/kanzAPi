using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class WishListRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("productId")]
    public int? Product { get; set; }
}
