namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridMessageActionRequest : SendGridDataRequest
{

    public string? Label { get; set; }

    public string? Url { get; set; }
}
