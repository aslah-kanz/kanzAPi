using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KanzApi.Messaging.Converters;

public class OtoDateTimeConverter : JsonConverter<DateTime>
{

    const string Pattern = "dd/MM/yyyy HH:mm";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string jsonValue = reader.GetString()!;
        DateTime.TryParseExact(jsonValue, Pattern, null, DateTimeStyles.None, out DateTime parsedDate);
        return parsedDate;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string jsonValue = value.ToString(Pattern);
        writer.WriteStringValue(jsonValue);
    }
}
