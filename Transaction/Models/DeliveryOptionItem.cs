namespace KanzApi.Transaction.Models;

public class DeliveryOptionItem : DeliveryDetail
{

    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public int? MinEstimatedDay { get; set; }

    public int? MaxEstimatedDay { get; set; }

    public decimal? EstimatedCost { get; set; }
}
