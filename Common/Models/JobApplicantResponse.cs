using System.Text.Json.Serialization;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class JobApplicantResponse
{

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? CountryCode { get; set; }

    public string PhoneNumber { get; set; } = "";

    [JsonPropertyName("applyDate")]
    public DateTime CreatedAt { get; set; }

    public EJobApplicantStatus? Status { get; set; }

    public JobResponse Job { get; set; } = new();

    public DocumentResponse? Document { get; set; }
}
