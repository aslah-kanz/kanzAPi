namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridMessageRequest : SendGridDataRequest
{

    public string? Title { get; set; }

    public List<string>? Messages { get; set; }

    public SendGridMessageActionRequest? Action { get; set; }

    public static SendGridMessageRequest From(string title, params string[] messages)
    {
        return new()
        {
            Subject = title,
            Title = title,
            Messages = [.. messages]
        };
    }

    public static SendGridMessageRequest From(string title, SendGridMessageActionRequest action, params string[] messages)
    {
        return new()
        {
            Subject = title,
            Title = title,
            Messages = [.. messages],
            Action = action
        };
    }
}
