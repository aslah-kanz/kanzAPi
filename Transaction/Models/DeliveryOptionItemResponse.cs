namespace KanzApi.Transaction.Models;

public class DeliveryOptionItemResponse
{

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public override bool Equals(object? obj)
    {
        DeliveryOptionItemResponse? other = obj as DeliveryOptionItemResponse;
        return other != null && (Name?.Equals(other.Name) ?? false);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
