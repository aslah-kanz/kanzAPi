namespace KanzApi.Transaction.Models;

public class DeliveryOption
{

    public int? Id { get; set; }

    public int? MinEstimatedDay { get; set; } = 0;

    public int? MaxEstimatedDay { get; set; } = 0;

    public decimal? EstimatedCost { get; set; } = 0;

    public List<DeliveryOptionItem> Items { get; set; } = [];
}
