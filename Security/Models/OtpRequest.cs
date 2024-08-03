using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Security.Models;

public class OtpRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [FixedSize(6, ErrorMessageResourceName = "FixedLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Code { get; set; }
}
