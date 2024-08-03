using KanzApi.Vendors.TinyUrl.Models;

namespace KanzApi.Vendors.TinyUrl.Services;

public interface ITinyUrlDefaultService
{

    TinyUrlResponse<TinyUrlCreateResponse> Create(TinyUrlCreateRequest request);

    TinyUrlResponse<TinyUrlCreateResponse> Create(string url);
}
