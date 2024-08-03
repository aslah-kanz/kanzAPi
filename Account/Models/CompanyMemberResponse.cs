using KanzApi.Account.Entities;
using System.Text.Json.Serialization;

namespace KanzApi.Account.Models;

public class CompanyMemberResponse
{

    public int Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    [JsonPropertyName("roleIds")]
    public ISet<int?> Roles { get; set; } = new HashSet<int?>();

    [JsonPropertyName("departmentIds")]
    public ISet<int?> Departments { get; set; } = new HashSet<int?>();

    public EPrincipalType? Type { get; set; }

    public EPrincipalStatus? Status { get; set; }
}
