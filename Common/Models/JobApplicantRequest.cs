using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class JobApplicantRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Name { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    [EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Email { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Numeric(ErrorMessageResourceName = "Numeric", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(4, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? CountryCode { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Numeric(ErrorMessageResourceName = "Numeric", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [DocumentFile(ErrorMessageResourceName = "DocumentFile", ErrorMessageResourceType = typeof(ValidationMessages))]
    public IFormFile? File { get; set; }
}
