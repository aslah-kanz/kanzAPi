using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Security.Models;

public class ResetPasswordRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Token { get; set; }

    [Password(ErrorMessageResourceName = "Password", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Password { get; set; }
}
