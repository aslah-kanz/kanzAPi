using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.TinyUrl.Models;

public class TinyUrlResponse<T> : IHttpClientResponse
{

    private int _code;

    public int Code { get { return _code; } set { _code = value; } }

    public List<string> Errors { get; set; } = [];

    public T? Data { get; set; }

    public bool IsSuccess()
    {
        return _code == 0;
    }
}
