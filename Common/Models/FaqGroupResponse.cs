using KanzApi.Common.Entities;

namespace KanzApi.Common.Models;

public class FaqGroupResponse
{

    public int Id { get; set; }

    public string Code { get; set; } = "";

    public LocalizableString Title { get; set; } = new();

    public LocalizableString? Description { get; set; }

    public bool ShowAtHomePage { get; set; }

    public ISet<FaqGroupFaqResponse> Faqs { get; set; } = new HashSet<FaqGroupFaqResponse>();

    public EPublishableStatus? Status { get; set; }
}
