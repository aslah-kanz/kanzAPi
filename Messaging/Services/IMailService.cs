using KanzApi.Account.Entities;
using KanzApi.Security.Entities;
using KanzApi.Transaction.Entities;

namespace KanzApi.Messaging.Services;

public interface IMailService
{

    void Send(Principal principal, OneTimeToken token);

    void Send(Principal principal, string password);

    void Send(Principal principal, bool approved);

    void Send(PurchaseQuote purchaseQuote);

    void Send(CustomerOrder customerOrder);
}
