
using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using System.Text.Json.Serialization;

namespace KanzApi.Account.Models;

public class StoreResponse
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int? SaleItemCount { get; set; }

    public EStoreType? Type { get; set; }

    public EActivableStatus? Status { get; set; }

    [JsonPropertyName("employees")]
    public ISet<StorePrincipalRequest?> Principals { get; set; } = new HashSet<StorePrincipalRequest?>();
}
