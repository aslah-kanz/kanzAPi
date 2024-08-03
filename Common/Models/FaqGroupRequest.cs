using KanzApi.Common.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class FaqGroupRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(160, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Title { get; set; }

    [MaxSize(4000, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Description { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public bool? ShowAtHomePage { get; set; } = false;

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

}
