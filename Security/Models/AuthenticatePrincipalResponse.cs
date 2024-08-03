using KanzApi.Account.Entities;

namespace KanzApi.Account.Models;

public class AuthenticatePrincipalResponse
{

    public int Id { get; set; }

    public string Username { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    public EPrincipalType? Type { get; set; }

    public EPrincipalStatus? Status { get; set; }
}
