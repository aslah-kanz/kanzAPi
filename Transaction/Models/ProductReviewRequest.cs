using KanzApi.Common.Validation;
using KanzApi.Resources;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Transaction.Models;

public class ProductReviewRequest
{

    [Range(0, 5, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int? Rating { get; set; }

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Comment { get; set; }

    [ImageFile(ErrorMessageResourceName = "ImageFile", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(5, ErrorMessageResourceName = "MaxSize", ErrorMessageResourceType = typeof(ValidationMessages))]
    public IFormFile[] Files { get; set; } = [];
}
