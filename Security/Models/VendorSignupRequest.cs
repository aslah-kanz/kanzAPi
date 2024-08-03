using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Resources;

namespace KanzApi.Security.Models;

public class VendorSignupRequest : SignupRequest
{

    [JsonIgnore]
    public new string? Password { get; set; }

    [JsonIgnore]
    public new EPrincipalType? Type { get; set; }

    [Required(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public PrincipalDetailRequest? Detail { get; set; }
}
