namespace KanzApi.Messaging.Models;

public class VoidResponse : IHttpClientResponse
{

    public bool IsSuccess()
    {
        return true;
    }
}
