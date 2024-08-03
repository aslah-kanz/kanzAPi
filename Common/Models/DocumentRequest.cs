using System.ComponentModel.DataAnnotations;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Common.Models;

public class DocumentRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [DocumentFile(ErrorMessageResourceName = "DocumentFile", ErrorMessageResourceType = typeof(ValidationMessages))]
    public IFormFile? File { get; set; }

    [NullOrNotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Name { get; set; }
}
