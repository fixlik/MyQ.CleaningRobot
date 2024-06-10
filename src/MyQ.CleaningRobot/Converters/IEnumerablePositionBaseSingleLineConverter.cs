using System.Text.Json.Serialization;
using System.Text.Json;
using MyQ.CleaningRobot.Entities;

namespace MyQ.CleaningRobot.Converters;

/// <summary>
/// Custom JSON converter for serializing and deserializing IEnumerable<PositionBase> objects.
/// </summary>
public class IEnumerablePositionBaseSingleLineConverter : JsonConverter<IEnumerable<PositionBase>>
{
    /// <summary>
    /// Reads the JSON representation of the object.
    /// This method is not implemented and will throw a NotImplementedException if called.
    /// </summary>
    /// <param name="reader">The reader used to read the JSON data.</param>
    /// <param name="typeToConvert">The type of the object to convert.</param>
    /// <param name="options">The serializer options.</param>
    /// <returns>The deserialized IEnumerable<PositionBase> object.</returns>
    public override IEnumerable<PositionBase> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The writer used to write the JSON data.</param>
    /// <param name="value">The IEnumerable<PositionBase> object to serialize.</param>
    /// <param name="options">The serializer options.</param>
    public override void Write(Utf8JsonWriter writer, IEnumerable<PositionBase> value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(string.Join(", ", JsonSerializer.Serialize(value)));
    }
}
