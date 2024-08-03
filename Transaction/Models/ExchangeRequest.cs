using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Common.Validation;
using KanzApi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Models;

public class ExchangeRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [BindProperty(Name = "purchaseQuoteId")]
    [JsonPropertyName("purchaseQuoteId")]
    public Guid? PurchaseQuote { get; set; }

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? Quantity { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [BindProperty(Name = "reason")]
    [JsonPropertyName("reason")]
    public string? Comment { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [ImageFile(ErrorMessageResourceName = "ImageFile", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(5, ErrorMessageResourceName = "MaxSize", ErrorMessageResourceType = typeof(ValidationMessages))]
    public IFormFile[] Files { get; set; } = [];
}
