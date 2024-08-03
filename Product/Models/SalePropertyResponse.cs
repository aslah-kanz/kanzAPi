using System.Text.Json.Serialization;
using KanzApi.Common.Models;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Models;

public class SalePropertyResponse : IComparable<SalePropertyResponse>
{

    public LocalizableString Name { get; set; } = new();

    public EPropertyType Type { get; set; }

    public IEnumerable<LocalizableString> Fields { get; set; } = [];

    public ISet<SalePropertyGroupResponse> Groups { get; set; } = new SortedSet<SalePropertyGroupResponse>();

    [JsonIgnore]
    public int SortOrder { get; set; }

    public int CompareTo(SalePropertyResponse? other)
    {
        return SortOrder.CompareTo(other?.SortOrder);
    }
}
