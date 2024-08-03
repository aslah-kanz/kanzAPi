using System.Text.Json.Serialization;
using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class SalePropertyItemResponse : IComparable<SalePropertyItemResponse>
{

    public LocalizableString Description { get; set; } = new();

    public LocalizableString Value1 { get; set; } = new();

    public LocalizableString? Unit1 { get; set; }

    public LocalizableString? Value2 { get; set; }

    public LocalizableString? Unit2 { get; set; }

    public LocalizableString? Value3 { get; set; }

    public LocalizableString? Unit3 { get; set; }

    public ImageResponse? Image { get; set; }

    [JsonIgnore]
    public int SortOrder { get; set; }

    public int CompareTo(SalePropertyItemResponse? other)
    {
        return SortOrder.CompareTo(other?.SortOrder);
    }
}
