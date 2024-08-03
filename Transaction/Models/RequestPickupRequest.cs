using System.ComponentModel.DataAnnotations;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class RequestPickupRequest
{

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Range(1, 100, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? PackageCount { get; set; } = 1;
}
