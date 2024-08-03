using KanzApi.Vendors.Msegat.Models;

namespace KanzApi.Vendors.Msegat.Services;

public interface IMsegatOtpService
{

    MsegatOtpResponse Send(MsegatOtpRequest request);

    MsegatResponse Verify(MsegatVerifyOtpRequest request);
}
