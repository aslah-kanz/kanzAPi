using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class FaqResponse
{

    public int Id { get; set; }

    public int? FaqGroupId { get; set; }

    public string? Code { get; set; }

    public LocalizableString Question { get; set; } = new();

    public LocalizableString? Answer { get; set; }

    public EPublishableStatus Status { get; set; }
}
