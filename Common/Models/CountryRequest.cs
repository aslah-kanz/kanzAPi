using System.Text.Json.Serialization;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class CountryRequest
{

    [MaxSize(2, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Code { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Name { get; set; }

    public int PhoneCode { get; set; }

    public int PhoneStartNumber { get; set; }

    public int PhoneMinLength { get; set; }

    public int PhoneMaxLength { get; set; }

    [JsonPropertyName("imageId")]
    public long? Image { get; set; }
}
