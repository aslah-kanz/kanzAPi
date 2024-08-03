using KanzApi.Account.Entities;

namespace KanzApi.Account.Models;

public interface IMemberRequest
{

    string? FirstName { get; set; }

    string? LastName { get; set; }

    string? Email { get; set; }

    string? CountryCode { get; set; }

    string? PhoneNumber { get; set; }

    EPrincipalType? Type { get; set; }
}
