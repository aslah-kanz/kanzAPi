using KanzApi.Account.Entities;
using KanzApi.Messaging.Models;
using KanzApi.Utils;
using KanzApi.Vendors.SendGrid.Models;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.SendGrid.Services;

public class SendGridMailService(IHttpClientFactory httpClientFactory,
ILogger<SendGridMailService> logger, IConfiguration configuration)
: SendGridService(httpClientFactory, logger), ISendGridMailService
{

    private const string MessageTemplateId = "d-e6abc731e27b4191a0542863fc111eba";

    private const string ReceiptTemplateId = "d-e7f27023834a485a97c153ca5ade530f";

    private readonly IConfiguration _configuration = configuration;

    public VoidResponse Send<T>(Principal to, string templateId, T request)
    where T : SendGridDataRequest
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.SendGridClient);

        string token = _configuration.GetValue<string>("SendGrid:ApiKey")!;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        SendGridRequest<T> rootRequest = new()
        {
            From = new()
            {
                Email = _configuration.GetValue<string>("KanzApi:Mail:Sender")!,
                Name = "Kanzway"
            },
            Personalizations = [
                new() {
                    To = [SendGridPersonRequest.From(to)],
                    DynamicTemplateData = request
                }
            ],
            TemplateId = templateId
        };

        return Post<SendGridRequest<T>, VoidResponse>(client, "/v3/mail/send", rootRequest)!;
    }

    public VoidResponse Send(Principal to, SendGridMessageRequest request)
    {
        return Send(to, MessageTemplateId, request);
    }

    public VoidResponse Send(Principal to, SendGridReceiptRequest request)
    {
        return Send(to, ReceiptTemplateId, request);
    }
}
