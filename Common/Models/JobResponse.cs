using System.Text.Json.Serialization;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class JobResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Title { get; set; } = new();

    public string? Slug { get; set; }

    public LocalizableString? Responsibility { get; set; }

    public string? Requirement { get; set; }

    public LocalizableString JobType { get; set; } = new();

    public LocalizableString? Experience { get; set; }

    public LocalizableString? JobLocation { get; set; }

    public JobFieldResponse? JobField { get; set; }

    public string? MetaKeyword { get; set; }

    public string? MetaDescription { get; set; }

    public EPublishableStatus? Status { get; set; }

    [JsonPropertyName("jobDate")]
    public DateTime UpdatedAt { get; set; }
}
