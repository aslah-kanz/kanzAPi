using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Extensions;
using KanzApi.Resources;
using KanzApi.Security.Entities;
using KanzApi.Transaction.Entities;
using KanzApi.Vendors.Graph.Services;
using KanzApi.Vendors.SendGrid.Models;
using KanzApi.Vendors.SendGrid.Services;
using Microsoft.Extensions.Localization;

namespace KanzApi.Messaging.Services;

public class MailService(IGraphMailService graphMailService, ISendGridMailService sendGridMailService,
IConfiguration configuration, IStringLocalizer<Messages> localizer) : IMailService
{

    private readonly IGraphMailService _graphMailService = graphMailService;

    private readonly ISendGridMailService _sendGridMailService = sendGridMailService;

    private readonly IConfiguration _configuration = configuration;

    private readonly IStringLocalizer<Messages> _localizer = localizer;

    public void Send(string title, string template, Dictionary<string, object> values, List<string> recipients)
    {
        if (!values.ContainsKey("title")) values["title"] = title;

        string htmlContent = File.ReadAllText($"./Resources/Templates/{template}.html");
        string content = htmlContent.Format(values);

        _graphMailService.Send(title, content, recipients);
    }

    public void Send(Principal principal, OneTimeToken token)
    {
        string baseUrl = _configuration.GetValue<string>("WebClient:BaseUrl")!;
        if (token.Type == EOneTimeTokenType.Activation)
        {
            Send("Account Activation", "account-activation", new() {
                { "webUrl", $"{baseUrl}/activation?token={Uri.EscapeDataString(token.Token!)}" }
            }, [principal.Email!]);
        }
        else if (token.Type == EOneTimeTokenType.ResetPassword)
        {
            Send("Password Has Been Reset", "password-reset", new() {
                { "username", principal.Username! },
                { "webUrl", $"{baseUrl}/reset-password?token={Uri.EscapeDataString(token.Token!)}" }
            }, [principal.Email!]);
        }
    }

    public void Send(Principal principal, string password)
    {
        string baseUrl = _configuration.GetValue<string>(principal.WebConfig + ":BaseUrl")!;
        Send("Account Has Been Activated", "password-generated", new() {
            { "username", principal.Username! },
            { "password", password },
            { "webUrl", $"{baseUrl}/en/login" }
        }, [principal.Email!]);
    }

    public void Send(Principal principal, bool approved)
    {
        if (approved)
        {
            string baseUrl = _configuration.GetValue<string>(principal.WebConfig + ":BaseUrl")!;
            _sendGridMailService.Send(principal, SendGridMessageRequest.From(
                _localizer.GetString("AccountApprovedMailTitle"),
                new SendGridMessageActionRequest()
                {
                    Label = _localizer.GetString("AccountApprovedMailAction"),
                    Url = $"{baseUrl}/en/login"
                },
                _localizer.GetString("AccountApprovedMailMessage")
            ));
        }
        else
        {
            _sendGridMailService.Send(principal, SendGridMessageRequest.From(
                _localizer.GetString("AccountRejectedMailTitle"),
                _localizer.GetString("AccountRejectedMailMessage")
            ));
        }
    }

    public void Send(PurchaseQuote purchaseQuote)
    {
        StoreOrder order = purchaseQuote.StoreOrder!;
        Store store = purchaseQuote.Store!;
        Principal principal = store.Principal!;

        if (purchaseQuote.Status == EPurchaseQuoteStatus.Open)
        {
            string baseUrl = _configuration.GetValue<string>("VendorClient:BaseUrl")!;
            _sendGridMailService.Send(principal, SendGridMessageRequest.From(
                _localizer.GetString("OrderOpenedMailTitle"),
                new SendGridMessageActionRequest()
                {
                    Label = _localizer.GetString("OrderOpenedMailAction"),
                    Url = $"{baseUrl}/en/order-management/{order.InvoiceNumber}"
                },
                _localizer.GetString("OrderOpenedMailMessage1"),
                _localizer.GetString("OrderOpenedMailMessage2", order.InvoiceNumber!),
                _localizer.GetString("OrderOpenedMailMessage3", store.Name!),
                _localizer.GetString("OrderOpenedMailMessage4", purchaseQuote.UpdatedAt!)
            ));
        }
        else if (purchaseQuote.Status == EPurchaseQuoteStatus.Delivered)
        {
            _sendGridMailService.Send(principal, SendGridMessageRequest.From(
                _localizer.GetString("OrderDeliveredMailTitle"),
                _localizer.GetString("OrderDeliveredMailMessage1"),
                _localizer.GetString("OrderDeliveredMailMessage2", order.InvoiceNumber!),
                _localizer.GetString("OrderDeliveredMailMessage3", store.Name!),
                _localizer.GetString("OrderDeliveredMailMessage4", purchaseQuote.UpdatedAt!)
            ));
        }
    }

    public void Send(CustomerOrder customerOrder)
    {
        string baseImageUrl = _configuration.GetValue<string>("AzureStorage:BaseUrl")!;
        _sendGridMailService.Send(customerOrder.Principal!, SendGridReceiptRequest.From(
            "Order Has Been Paid", customerOrder, baseImageUrl));
    }
}
