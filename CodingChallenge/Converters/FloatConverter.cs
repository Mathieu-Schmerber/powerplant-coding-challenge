using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodingChallenge.Converters;

/// <summary>
/// Json converter for float.
/// Formats the float value to 1 decimal place. 
/// </summary>
public class FloatConverter : JsonConverter<float>
{
    ///<inheritdoc />
    public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetSingle();

    ///<inheritdoc />
    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("0.0"));
}