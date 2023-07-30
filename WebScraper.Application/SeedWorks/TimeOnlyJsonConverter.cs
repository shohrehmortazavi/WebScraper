using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebScraper.Application.SeedWorks
{
    public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.FromDateTime(reader.GetDateTime());
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            var time = value.ToString("HH:mm:ss");
            writer.WriteStringValue(time);
        }
    }
}
