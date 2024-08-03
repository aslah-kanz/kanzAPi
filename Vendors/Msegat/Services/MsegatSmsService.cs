using KanzApi.Account.Entities;
using KanzApi.Utils;
using KanzApi.Vendors.Msegat.Models;

namespace KanzApi.Vendors.Msegat.Services;

public class MsegatSmsService(IHttpClientFactory httpClientFactory,
ILogger<MsegatSmsService> logger, IConfiguration configuration)
: MsegatService(httpClientFactory, logger), IMsegatSmsService
{

    private readonly IConfiguration _configuration = configuration;

    private void Init(MsegatRequest request)
    {
        request.Username = _configuration.GetValue<string>("Msegat:Username");
        request.ApiKey = _configuration.GetValue<string>("Msegat:ApiKey");
        request.UserSender = _configuration.GetValue<string>("Msegat:Sender");
    }

    public MsegatResponse Send(MsegatSmsRequest request)
    {
        Init(request);

        return Post<MsegatSmsRequest, MsegatResponse>(
            Constants.MsegatClient, "/gw/sendsms.php", request)!;
    }

    public MsegatResponse Send(Principal principal, string message)
    {
        return Send(new()
        {
            Number = principal.FullPhoneNumber,
            Message = message
        });
    }
}
