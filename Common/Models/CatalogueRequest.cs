using System.Text.Json.Serialization;
using KanzApi.Common.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class CatalogueRequest
{

    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Title { get; set; }

    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Slug { get; set; }

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Description { get; set; }

    [JsonPropertyName("imageId")]
    public long? Image { get; set; }

    [JsonPropertyName("documentId")]
    public long? Document { get; set; }

    [MaxSize(65, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaKeyword { get; set; }

    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? MetaDescription { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;
}
