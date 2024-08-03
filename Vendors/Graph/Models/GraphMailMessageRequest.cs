using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class GraphMailMessageRequest
{

    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("body")]
    public GraphMailBodyRequest? Body { get; set; }

    [JsonPropertyName("toRecipients")]
    public List<GraphMailRecipientRequest>? Recipients { get; set; }
}
