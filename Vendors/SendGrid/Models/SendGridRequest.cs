using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridRequest<T> : IHttpClientRequest
where T : SendGridDataRequest
{

    public SendGridPersonRequest? From { get; set; }

    public List<SendGridPersonalizationRequest<T>>? Personalizations { get; set; }

    [JsonPropertyName("template_id")]
    public string? TemplateId { get; set; }
}
