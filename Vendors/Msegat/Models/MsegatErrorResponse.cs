using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatErrorResponse : IHttpClientResponse
{

    public string? Code { get; set; }

    public string? Message { get; set; }

    public bool IsSuccess()
    {
        throw new NotImplementedException();
    }
}
