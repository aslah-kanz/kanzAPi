using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Common.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class FaqRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("faqGroupId")]
    public int? FaqGroup { get; set; }

    [MaxSize(4000, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Question { get; set; }

    [MaxSize(4000, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Answer { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;
}
