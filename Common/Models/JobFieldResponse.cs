namespace KanzApi.Common.Models;

public class JobFieldResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString Name { get; set; } = new();
}
