using System.Text.Json;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatResponse : IHttpClientResponse
{

    public const string Success = "1";

    public JsonElement? Code { get; set; }

    public string? Message { get; set; }

    public bool IsSuccess()
    {
        string? code = Code?.ToString();
        return code?.Equals(Success) ?? false;
    }
}
