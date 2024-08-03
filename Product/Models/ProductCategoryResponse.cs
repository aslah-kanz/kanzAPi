using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class ProductCategoryResponse
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string Slug { get; set; } = "";
}
