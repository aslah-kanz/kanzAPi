
namespace KanzApi.Common.Models;

public class FaqGroupFaqResponse
{

    public int Id { get; set; }

    public string Code { get; set; } = "";

    public LocalizableString? Question { get; set; }

    public LocalizableString? Answer { get; set; }
}
