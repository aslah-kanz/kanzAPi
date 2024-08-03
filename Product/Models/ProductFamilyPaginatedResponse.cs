using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class ProductFamilyPaginatedResponse : PaginatedResponse<string>
{

    public int ProductCount { get; set; }

    public static ProductFamilyPaginatedResponse From(
        PaginatedEntity<string?> pEntity, int productCount)
    {
        return new()
        {
            Content = pEntity.Content.Select(code => code!),
            Page = pEntity.Page,
            Size = pEntity.Size,
            TotalCount = pEntity.Count,
            ProductCount = productCount
        };
    }
}
