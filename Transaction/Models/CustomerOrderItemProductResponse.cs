using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class CustomerOrderItemProductResponse
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();

    public ImageResponse? Image { get; set; }
}
