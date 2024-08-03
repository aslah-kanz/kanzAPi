using KanzApi.Account.Entities;
using KanzApi.Common.Validation;
using KanzApi.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KanzApi.Account.Models;

public class CompanyMemberRequest : IMemberRequest
{

    [JsonIgnore]
    public EPrincipalType? Type { get; set; } = EPrincipalType.Company;

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? FirstName { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? LastName { get; set; }

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

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("roleIds")]
    public ISet<int>? Roles { get; set; }

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [JsonPropertyName("departmentIds")]
    public ISet<int>? Departments { get; set; }
}
