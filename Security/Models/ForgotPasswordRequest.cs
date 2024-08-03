using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Security.Models;

public class ForgotPasswordRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    [EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Email { get; set; }
}
