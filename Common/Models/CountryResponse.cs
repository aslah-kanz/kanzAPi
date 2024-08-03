namespace KanzApi.Common.Models;

public class CountryResponse
{

    public int Id { get; set; }

    public string Code { get; set; } = "";

    public LocalizableString Name { get; set; } = new();

    public int PhoneCode { get; set; }

    public int PhoneStartNumber { get; set; }

    public int PhoneMinLength { get; set; }

    public int PhoneMaxLength { get; set; }

    public ImageResponse? Image { get; set; }
}
