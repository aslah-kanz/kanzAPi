using System.Text.Json.Serialization;
using KanzApi.Account.Entities;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoPickupLocationRequest : IHttpClientRequest
{

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("contactName")]
    public string? ContactName { get; set; }

    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }

    [JsonPropertyName("lat")]
    public decimal? Latitude { get; set; }

    [JsonPropertyName("lon")]
    public decimal? Longitude { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("postcode")]
    public string? PostCode { get; set; }

    [JsonPropertyName("servingRadius")]
    public int? ServingRadius { get; set; }

    [JsonPropertyName("brandName")]
    public string? BrandName { get; set; }

    public static OtoPickupLocationRequest From(Store store)
    {
        Principal principal = store.Principal!;
        return new()
        {
            Type = "warehouse",
            Code = store.Code,
            Name = store.OtoStoreName,
            Mobile = principal.FullPhoneNumber,
            Address = store.Address,
            ContactName = principal.FullName,
            ContactEmail = principal.Email,
            Latitude = store.Latitude,
            Longitude = store.Longitude,
            City = store.City,
            Country = store.Country,
            BrandName = store.Name
        };
    }
}
