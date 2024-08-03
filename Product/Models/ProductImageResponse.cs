using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class ProductImageResponse
{

    public int Id { get; set; }

    public ImageResponse? Image { get; set; }

    public int SortOrder { get; set; }
}
