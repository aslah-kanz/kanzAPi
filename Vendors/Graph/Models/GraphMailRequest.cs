using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class GraphMailRequest
{

    [JsonPropertyName("message")]
    public GraphMailMessageRequest? Message { get; set; }

    public static GraphMailRequest From(string subject, string content, List<string> recipients)
    {
        return new()
        {
            Message = new()
            {
                Subject = subject,
                Body = new()
                {
                    Content = content
                },
                Recipients = recipients.Select(recipient => new GraphMailRecipientRequest()
                {
                    EmailAddress = new() { Address = recipient }
                }).ToList()
            }
        };
    }
}
