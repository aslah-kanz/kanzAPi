
namespace KanzApi.Common.Models;

public class LocalizableNameResponse
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();
}
