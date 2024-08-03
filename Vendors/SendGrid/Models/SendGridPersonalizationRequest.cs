using System.Text.Json.Serialization;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridPersonalizationRequest<T>
where T : SendGridDataRequest
{

    public List<SendGridPersonRequest>? To { get; set; }

    [JsonPropertyName("dynamic_template_data")]
    public T? DynamicTemplateData { get; set; }
}
