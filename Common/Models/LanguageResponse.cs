namespace KanzApi.Common.Models;

public class LanguageResponse
{

    public int Id { get; set; }

    public string Code { get; set; } = "";

    public LocalizableString Name { get; set; } = new();

    public ImageResponse? Image { get; set; }
}
