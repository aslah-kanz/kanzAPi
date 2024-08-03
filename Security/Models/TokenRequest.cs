using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Security.Models;

public class TokenRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public ISet<int>? PrivilegeIds { get; set; } = new HashSet<int>();

    [Required(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Range(0, Int32.MaxValue, ErrorMessageResourceName = "Min", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? Validity { get; set; }
}
