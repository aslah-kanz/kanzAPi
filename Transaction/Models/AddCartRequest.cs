using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class AddCartRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("productId")]
    public int? Product { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public ISet<int> SaleItemIds { get; set; } = new HashSet<int>();

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    public decimal Price { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Range(1, Int32.MaxValue, ErrorMessageResourceName = "Min", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int Quantity { get; set; } = 0;

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Comment { get; set; }
}
