namespace KanzApi.Account.Models;

public class PrincipalAddressItem
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Address { get; set; } = "";

    public string RecipientName { get; set; } = "";

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    public string City { get; set; } = "";

    public string Country { get; set; } = "";

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool DefaultAddress { get; set; }
}
