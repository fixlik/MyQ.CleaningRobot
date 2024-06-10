using MyQ.CleaningRobot.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyQ.CleaningRobot.Converters;

/// <summary>
/// Converts the InputOutputPosition class to a single line JSON representation and vice versa.
/// </summary>
public class InputOutputPositionSingleLineConverter : JsonConverter<InputOutputPosition>
{
    /// <summary>
    /// Reads the JSON representation of the InputOutputPosition object.
    /// </summary>
    /// <param name="reader">The reader used to read the JSON data.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized InputOutputPosition object.</returns>
    public override InputOutputPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Writes the JSON representation of the InputOutputPosition object.
    /// </summary>
    /// <param name="writer">The writer used to write the JSON data.</param>
    /// <param name="value">The InputOutputPosition object to serialize.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, InputOutputPosition value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(JsonSerializer.Serialize(value));
    }
}


