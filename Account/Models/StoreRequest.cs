using KanzApi.Account.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Account.Models;

public class StoreRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Name { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Address { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? City { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Country { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Range(-90, 90, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
    public decimal? Latitude { get; set; }

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    [Range(-180, 180, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
    public decimal? Longitude { get; set; }

    public EStoreType? Type { get; set; }

    public ISet<StorePrincipalRequest> Employees { get; set; } = new HashSet<StorePrincipalRequest>();
}
