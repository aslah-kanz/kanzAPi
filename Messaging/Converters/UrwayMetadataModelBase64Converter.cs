using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Messaging.Converters;

public class UrwayMetadataModelBase64Converter : JsonConverter<UrwayMetadataModel>
{

    public override UrwayMetadataModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string base64Value = reader.GetString()!;
        int padding = 4 - (base64Value.Length % 4);
        if (padding > 0 && padding < 4)
        {
            base64Value += new string('=', padding);
        }

        byte[] jsonBytes = Convert.FromBase64String(base64Value);
        string jsonValue = Regex.Unescape(Encoding.UTF8.GetString(jsonBytes));
        return JsonSerializer.Deserialize<UrwayMetadataModel>(jsonValue)!;
    }

    public override void Write(Utf8JsonWriter writer, UrwayMetadataModel value, JsonSerializerOptions options)
    {
        string jsonValue = JsonSerializer.Serialize(value);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonValue);
        string base64Value = Convert.ToBase64String(jsonBytes);
        writer.WriteStringValue(base64Value);
    }
}
