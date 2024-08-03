using KanzApi.Account.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class CustomerProfileResponse
{

    public EPrincipalType? Type { get; set; }

    public string Username { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    public EGender? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public EPrincipalStatus? Status { get; set; }

    public ImageResponse? Image { get; set; }

    public double Wallet { get; set; }
}
