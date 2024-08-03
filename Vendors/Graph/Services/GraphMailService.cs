using KanzApi.Utils;
using KanzApi.Vendors.Graph.Models;
using KanzApi.Vendors.Msegat.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace KanzApi.Vendors.Graph.Services;

public class GraphMailService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
ILogger<GraphMailService> logger) : IGraphMailService
{

    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    private readonly IConfiguration _configuration = configuration;

    private readonly ILogger<GraphMailService> _logger = logger;

    private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = false };

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

    public GraphAccessTokenResponse GetAccessToken()
    {
        string clientId = _configuration.GetValue<string>("Graph:Mail:ClientId")!;
        string clientSecret = _configuration.GetValue<string>("Graph:Mail:ClientSecret")!;
        string tenantId = _configuration.GetValue<string>("Graph:Mail:TenantId")!;

        FormUrlEncodedContent content = new([
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        ]);

        using HttpClient client = _httpClientFactory.CreateClient(Constants.GraphLoginClient);
        string url = $"/{tenantId}/oauth2/v2.0/token";
        HttpResponseMessage httpResponse = client.PostAsync(url, content).Result;
        Log(httpResponse);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new GraphUnknownException();
        }

        return httpResponse.Content.ReadFromJsonAsync<GraphAccessTokenResponse>().Result!;
    }

    public void Send(string title, string content, List<string> recipients)
    {
        GraphAccessTokenResponse accessToken = GetAccessToken();

        string sender = _configuration.GetValue<string>("KanzApi:Mail:Sender")!;
        string url = $"/v1.0/users/{Uri.EscapeDataString(sender)}/sendMail";

        string payload = JsonSerializer.Serialize(GraphMailRequest.From(title, content, recipients));
        StringContent httpContent = new(payload, Encoding.UTF8, MediaTypeNames.Application.Json);

        using HttpClient client = _httpClientFactory.CreateClient(Constants.GraphMailClient);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);

        HttpResponseMessage httpResponse = client.PostAsync(url, httpContent).Result;
        Log(httpResponse);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new GraphUnknownException();
        }
    }
}
