namespace KanzApi.Common.Models;

public class CurrencyItem
{

    public int Id { get; set; }

    public string Code { get; set; } = "";

    public string? Description { get; set; }

    public string? Symbol { get; set; }
}
