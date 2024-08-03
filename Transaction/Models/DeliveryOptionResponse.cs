namespace KanzApi.Transaction.Models;

public class DeliveryOptionResponse
{

    public int? Id { get; set; }

    public int? MinEstimatedDay { get; set; } = 0;

    public int? MaxEstimatedDay { get; set; } = 0;

    public decimal? EstimatedCost { get; set; } = 0;

    public ISet<DeliveryOptionItemResponse> Items { get; set; } = new HashSet<DeliveryOptionItemResponse>();
}
