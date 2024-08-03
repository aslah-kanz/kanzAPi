using KanzApi.Security.Entities;
using KanzApi.Transaction.Entities;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Vendors.Urway.Services;

public interface IUrwayTransactionService
{

    UrwayTransactionResponse Send(UrwayTransactionRequest request);

    UrwayTransactionResponse Send(CustomerOrder order, OneTimeToken token, string? redirectPath);
}
