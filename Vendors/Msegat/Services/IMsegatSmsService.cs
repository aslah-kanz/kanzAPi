using KanzApi.Account.Entities;
using KanzApi.Vendors.Msegat.Models;

namespace KanzApi.Vendors.Msegat.Services;

public interface IMsegatSmsService
{

    MsegatResponse Send(MsegatSmsRequest request);

    MsegatResponse Send(Principal principal, string message);
}
