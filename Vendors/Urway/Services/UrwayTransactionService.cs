using KanzApi.Account.Entities;
using KanzApi.Extensions;
using KanzApi.Security.Entities;
using KanzApi.Transaction.Entities;
using KanzApi.Utils;
using KanzApi.Vendors.Urway.Models;
using System.ComponentModel.DataAnnotations;

namespace KanzApi.Vendors.Urway.Services;

public class UrwayTransactionService(IHttpClientFactory httpClientFactory, ILogger<UrwayTransactionService> logger,
IServiceProvider serviceProvider, IConfiguration configuration)
: UrwayService(httpClientFactory, logger), IUrwayTransactionService
{

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly IConfiguration _configuration = configuration;

    public UrwayTransactionResponse Send(UrwayTransactionRequest request)
    {
        string secretKey = _configuration.GetValue<string>("Urway:SecretKey")!;

        request.TerminalId = _configuration.GetValue<string>("Urway:TerminalId");
        request.Action = "1";
        request.MerchantIp = _configuration.GetValue<string>("KanzApi:IpAddress");
        request.Password = _configuration.GetValue<string>("Urway:Password");
        request.Currency = "SAR";
        request.Country = "SA";
        request.RequestHash = (request.TrackId
        + "|" + request.TerminalId
        + "|" + request.Password
        + "|" + secretKey
        + "|" + request.Amount
        + "|" + request.Currency).Sha256();

        UrwayTransactionResponse response = Post<UrwayTransactionRequest, UrwayTransactionResponse>(
            Constants.UrwayClient, "/URWAYPGService/transaction/jsonProcess/JSONrequest", request)!;

        ValidationContext context = new(response, _serviceProvider, null);
        if (!Validator.TryValidateObject(response, context, null, true))
        {
            throw new UrwayHashMismatchException();
        }

        return response;
    }

    public UrwayTransactionResponse Send(CustomerOrder order, OneTimeToken token, string? redirectPath)
    {
        order.PaymentTrackId = GuidBase62.Encode((Guid)order.Id!);
        Principal principal = order.Principal!;

        Uri baseUri = new(_configuration.GetValue<string>("WebClient:BaseUrl")!);
        Uri redirectUri = !String.IsNullOrEmpty(redirectPath)
        ? new(baseUri, redirectPath) : baseUri;

        UrwayTransactionRequest request = new()
        {
            TrackId = order.PaymentTrackId,
            CustomerEmail = principal.Email,
            Amount = ((decimal)order.GrandTotal!).ToString("0.00"),
            MetaData = new() { Token = token.Token },
            Udf2 = redirectUri.ToString(),
            Udf5 = "iframe"
        };
        return Send(request);
    }
}
