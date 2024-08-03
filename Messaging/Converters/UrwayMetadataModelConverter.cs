using System.Text.Json;
using System.Text.Json.Serialization;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Messaging.Converters;

public class UrwayMetadataModelConverter : JsonConverter<UrwayMetadataModel>
{

    public override UrwayMetadataModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string jsonValue = reader.GetString()!;
        return JsonSerializer.Deserialize<UrwayMetadataModel>(jsonValue)!;
    }

    public override void Write(Utf8JsonWriter writer, UrwayMetadataModel value, JsonSerializerOptions options)
    {
        string jsonValue = JsonSerializer.Serialize(value);
        writer.WriteStringValue(jsonValue);
    }
}
