using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedClasses.JsonConverters
{
    public class FormattableStringConverter : JsonConverter<FormattableString>
    {
        public override FormattableString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, FormattableString value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Value", value.Format);

            var arguments = value.GetArguments();

            if (arguments.Any())
            {
                writer.WriteStartArray("Arguments");

                foreach (var argument in value.GetArguments())
                {
                    JsonSerializer.Serialize(writer, argument);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
