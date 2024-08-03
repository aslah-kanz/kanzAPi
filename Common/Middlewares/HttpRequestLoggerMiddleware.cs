using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace KanzApi.Common.Middlewares;

public class HttpRequestLoggerMiddleware(RequestDelegate next,
ILogger<HttpRequestLoggerMiddleware> logger, IConfiguration configuration)
{

    private readonly RequestDelegate _next = next;

    private readonly IConfiguration _configuration = configuration;

    private readonly ILogger<HttpRequestLoggerMiddleware> _logger = logger;

    private readonly JsonSerializerOptions _jsonOption = new() { WriteIndented = false };

    private string? Mask(string key, string? value)
    {
        if (!_configuration.GetValue<bool>("KanzApi:Logging:SensitiveDataMasked")) return value;
        List<string> maskedKeys = _configuration.GetSection("KanzApi:Logging:MaskedKeys").Get<List<string>>()!;

        Regex regex = new(key, RegexOptions.IgnoreCase);
        return maskedKeys.Any(k => regex.Match(k).Success) ? "**masked**" : value;
    }

    private void Mask(JsonArray arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            JsonNode? value = arr[i];
            if (value == null) continue;
            else if (value.GetValueKind() == JsonValueKind.Object) Mask(value.AsObject());
        }
    }

    private void Mask(JsonObject obj)
    {
        List<string> keys = [];
        foreach (KeyValuePair<string, JsonNode?> pair in obj)
        {
            JsonNode? value = pair.Value;
            if (value == null) continue;
            else if (value.GetValueKind() == JsonValueKind.Object) Mask(value.AsObject());
            else if (value.GetValueKind() == JsonValueKind.Array) Mask(value.AsArray());
            else keys.Add(pair.Key);
        }

        foreach (string key in keys)
        {
            string? v = Mask(key, null);
            if (v != null) obj[key] = v;
        }
    }

    private void Mask(JsonNode? node)
    {
        if (node == null) return;
        else if (node.GetValueKind() == JsonValueKind.Object) Mask(node.AsObject());
        else if (node.GetValueKind() == JsonValueKind.Array) Mask(node.AsArray());
    }

    private async Task<string> FilterJsonContent(HttpRequest request)
    {
        request.EnableBuffering();

        using MemoryStream memoryStream = new();
        await request.Body.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        string rawContent = await new StreamReader(memoryStream).ReadToEndAsync();
        request.Body.Position = 0;

        JsonNode? node = JsonNode.Parse(rawContent);
        Mask(node);

        return JsonSerializer.Serialize(node, _jsonOption);
    }

    private void Log(HttpContext context, string? content)
    {
        try
        {
            QueryString query = context.Request.QueryString;
            Dictionary<string, string> headers = context.Request.Headers.ToDictionary(e => e.Key, e =>
            {
                return Mask(e.Key, e.Value.ToString())!;
            });

            _logger.LogDebug("Request: {IpAddress} - [{Method}] {Path}{QueryString}\n  Headers: {Headers}\n  Content: {Content}",
            context.Connection.RemoteIpAddress?.ToString(), context.Request.Method, context.Request.Path, query.Value,
            JsonSerializer.Serialize(headers, _jsonOption), content);
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, "Request log error:");
        }
    }

    public async Task Invoke(HttpContext context)
    {
        string? content = null;

        HttpRequest request = context.Request;
        long length = request.ContentLength ?? 0;
        if (length > 0)
        {
            if (request.ContentType?.Contains(MediaTypeNames.Application.Json) ?? false)
            {
                content = await FilterJsonContent(request);
            }
            else if (!String.IsNullOrEmpty(request.ContentType))
            {
                content = "**" + request.ContentType + "**";
            }
        }

        Log(context, content);

        await _next(context);
    }
}
