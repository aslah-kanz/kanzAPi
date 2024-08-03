namespace KanzApi.Account.Models;

public class VendorStoreResponse
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
