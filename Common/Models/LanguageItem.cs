namespace KanzApi.Common.Models;

public class LanguageItem
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public LocalizableString? Name { get; set; }
    
    public ImageResponse? Image { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
