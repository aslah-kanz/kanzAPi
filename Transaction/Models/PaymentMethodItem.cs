using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class PaymentMethodItem
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString? Name { get; set; }

    public LocalizableString? Instruction { get; set; }

    public LocalizableString? Description { get; set; }

    public ImageResponse? Image { get; set; }

    public ImageResponse? ImageAr { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
