using KanzApi.Utils;
using KanzApi.Vendors.TinyUrl.Models;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.TinyUrl.Services;

public class TinyUrlDefaultService(IHttpClientFactory httpClientFactory,
ILogger<TinyUrlDefaultService> logger, IConfiguration configuration)
: TinyUrlService<TinyUrlCreateResponse>(httpClientFactory, logger), ITinyUrlDefaultService
{

    private readonly IConfiguration _configuration = configuration;

    public TinyUrlResponse<TinyUrlCreateResponse> Create(TinyUrlCreateRequest request)
    {
        request.Domain = _configuration.GetValue<string>("TinyUrl:Domain");

        using HttpClient client = _httpClientFactory.CreateClient(Constants.TinyUrlClient);

        string token = _configuration.GetValue<string>("TinyUrl:Token")!;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return Post<TinyUrlCreateRequest, TinyUrlResponse<TinyUrlCreateResponse>>(
            client, "/create", request)!;
    }

    public TinyUrlResponse<TinyUrlCreateResponse> Create(string url)
    {
        return Create(new TinyUrlCreateRequest() { Url = url });
    }
}
