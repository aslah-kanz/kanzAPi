using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface ICustomerProfileService : ICrudService<Principal, int?>
{

    CustomerProfileResponse Edit(CustomerProfileRequest request);

    CustomerProfileResponse GetModelById();
}
