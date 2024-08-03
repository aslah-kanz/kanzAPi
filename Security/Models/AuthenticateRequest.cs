using KanzApi.Account.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Security.Models;

public class AuthenticateRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Username { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Password { get; set; }

    [MaxSize(2, ErrorMessageResourceName = "MaxSize", ErrorMessageResourceType = typeof(ValidationMessages))]
    public ISet<EPrincipalType>? Types { get; set; } = new HashSet<EPrincipalType>();
}
