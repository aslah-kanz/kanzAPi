using KanzApi.Messaging.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace KanzApi.Messaging.Services;

public abstract class HttpClientService<T>(IHttpClientFactory httpClientFactory,
ILogger<HttpClientService<T>> logger,
bool hasNoResponse = false) where T : IHttpClientResponse
{

    protected readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    private readonly ILogger<HttpClientService<T>> _logger = logger;

    private readonly bool _hasNoResponse = hasNoResponse;

    private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = false };

    private readonly JsonSerializerOptions _options = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected virtual void Error(HttpResponseMessage httpResponse)
    {
        throw new Exception("Unhandled Exception");
    }

    protected virtual void Validate(HttpResponseMessage httpResponse)
    {
        if (!httpResponse.IsSuccessStatusCode) Error(httpResponse);
    }

    protected virtual void Error(T response)
    {
        throw new Exception("Unhandled Exception");
    }

    protected virtual void Validate(T response)
    {
        if (!response.IsSuccess()) Error(response);
    }

    private Dictionary<string, List<string>> ToDictionary(HttpHeaders headers)
    {
        Dictionary<string, List<string>> result = [];
        foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
        {
            result[header.Key] = header.Value.ToList();
        }
        return result;
    }

    private void Log(HttpResponseMessage response)
    {
        HttpRequestMessage request = response.RequestMessage!;
        _logger.LogDebug("HTTP Client: [{Method}] {Path}\n"
        + "  Request:\n    Headers: {RequestHeaders}\n    Content: {RequestContent}\n"
        + "  Response:\n    Headers: {ResponseHeaders}\n    Content: {ResponseContent}",
            request.Method, request.RequestUri!.AbsoluteUri,
            JsonSerializer.Serialize(ToDictionary(request.Headers), _jsonOption),
            request.Content?.ReadAsStringAsync().Result,
            JsonSerializer.Serialize(ToDictionary(response.Headers), _jsonOption),
            response.Content.ReadAsStringAsync().Result);
    }

    protected V? Get<V>(HttpClient client, string path)
    where V : T
    {
        HttpResponseMessage httpResponse = client.GetAsync(path).Result;
        Log(httpResponse);

        Validate(httpResponse);

        if (!_hasNoResponse)
        {
            V response = httpResponse.Content.ReadFromJsonAsync<V>().Result!;
            Validate(response);

            return response;
        }
        else
        {
            return default;
        }
    }

    public V? Get<V>(string name, string path) where V : T
    {
        using HttpClient client = _httpClientFactory.CreateClient(name);
        return Get<V>(client, path);
    }

    protected V? Post<U, V>(HttpClient client, string path, U request)
    where U : IHttpClientRequest
    where V : T
    {
        string content = JsonSerializer.Serialize(request, _options);

        StringContent httpContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        HttpResponseMessage httpResponse = client.PostAsync(path, httpContent).Result;
        Log(httpResponse);

        Validate(httpResponse);

        if (!_hasNoResponse)
        {
            V response = httpResponse.Content.ReadFromJsonAsync<V>().Result!;
            Validate(response);

            return response;
        }
        else
        {
            return default;
        }
    }

    public V? Post<U, V>(string name, string path, U request)
    where U : IHttpClientRequest
    where V : T
    {
        using HttpClient client = _httpClientFactory.CreateClient(name);
        return Post<U, V>(client, path, request);
    }
}
