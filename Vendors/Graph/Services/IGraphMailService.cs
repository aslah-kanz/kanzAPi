using KanzApi.Vendors.Graph.Models;

namespace KanzApi.Vendors.Graph.Services;

[Obsolete("Changed to SendGrid, check: KanzApi.Vendors.SendGrid.Services.ISendGridMailService")]
public interface IGraphMailService
{

    GraphAccessTokenResponse GetAccessToken();

    void Send(string title, string content, List<string> recipients);
}
