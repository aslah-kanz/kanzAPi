using KanzApi.Account.Entities;
using KanzApi.Messaging.Models;
using KanzApi.Vendors.SendGrid.Models;

namespace KanzApi.Vendors.SendGrid.Services;

public interface ISendGridMailService
{

    VoidResponse Send<T>(Principal to, string templateId, T request)
    where T : SendGridDataRequest;

    VoidResponse Send(Principal to, SendGridMessageRequest request);

    VoidResponse Send(Principal to, SendGridReceiptRequest request);
}
