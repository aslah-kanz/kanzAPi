using System.Text.Json.Serialization;
using KanzApi.Common.Models;

namespace KanzApi.Product.Models;

public class LinkedCategoryItem : IComparable<LinkedCategoryItem>
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();

    public string Slug { get; set; } = "";

    [JsonPropertyName("children")]
    public ISet<LinkedCategoryItem> Items { get; set; } = new SortedSet<LinkedCategoryItem>();

    public int CompareTo(LinkedCategoryItem? other)
    {
        if (other == null) return 1;

        return Name.En!.CompareTo(other.Name.En);
    }
}
