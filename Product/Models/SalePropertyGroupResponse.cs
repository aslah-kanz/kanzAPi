using System.Text.Json.Serialization;
using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class SalePropertyGroupResponse : IComparable<SalePropertyGroupResponse>
{

    public LocalizableString Name { get; set; } = new();

    public ISet<SalePropertyItemResponse> Items { get; set; } = new SortedSet<SalePropertyItemResponse>();

    [JsonIgnore]
    public int SortOrder { get; set; }

    public int CompareTo(SalePropertyGroupResponse? other)
    {
        return SortOrder.CompareTo(other?.SortOrder);
    }
}
