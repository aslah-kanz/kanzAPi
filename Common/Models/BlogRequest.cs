using System.Text.Json.Serialization;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class BlogRequest
{

    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Title { get; set; }

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Slug { get; set; }

    [JsonPropertyName("imageId")]
    public long? Image { get; set; }

    [MaxSize(4000, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Description { get; set; }

    [MaxSize(65, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaKeyword { get; set; }

    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaDescription { get; set; }
}
