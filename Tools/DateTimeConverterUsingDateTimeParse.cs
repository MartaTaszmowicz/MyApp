using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyApp;

public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.TryParseExact(reader.GetString() ?? string.Empty, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dt) ? dt : DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}