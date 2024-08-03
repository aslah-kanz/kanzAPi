using KanzApi.Utils;
using KanzApi.Vendors.Msegat.Models;

namespace KanzApi.Vendors.Msegat.Services;

public class MsegatOtpService(IHttpClientFactory httpClientFactory,
ILogger<MsegatOtpService> logger, IConfiguration configuration)
: MsegatService(httpClientFactory, logger), IMsegatOtpService
{

    private readonly IConfiguration _configuration = configuration;

    private void Init(MsegatRequest request)
    {
        request.Username = _configuration.GetValue<string>("Msegat:Username");
        request.ApiKey = _configuration.GetValue<string>("Msegat:ApiKey");
        request.UserSender = _configuration.GetValue<string>("Msegat:Sender");
    }

    public MsegatOtpResponse Send(MsegatOtpRequest request)
    {
        Init(request);
        return Post<MsegatOtpRequest, MsegatOtpResponse>(
            Constants.MsegatClient, "/gw/sendOTPCode.php", request)!;
    }

    public MsegatResponse Verify(MsegatVerifyOtpRequest request)
    {
        Init(request);
        return Post<MsegatVerifyOtpRequest, MsegatResponse>(
            Constants.MsegatClient, "/gw/verifyOTPCode.php", request)!;
    }
}
