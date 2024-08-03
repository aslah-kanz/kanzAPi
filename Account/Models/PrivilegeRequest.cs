using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Account.Models;

public class PrivilegeRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Name { get; set; }
}
