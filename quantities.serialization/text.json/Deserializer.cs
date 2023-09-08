using System.Text.Json;
using static System.Text.Json.JsonTokenType;

namespace Quantities.Serialization.Text.Json;

internal static class Deserializer
{
    public static Boolean MoveNext(this ref Utf8JsonReader reader, JsonTokenType tokenType)
    {
        Boolean readNext = true;
        while (reader.TokenType != tokenType && (readNext = reader.Read())) { }
        return readNext;
    }

    public static String ReadNameOf(this ref Utf8JsonReader reader, JsonTokenType tokenType)
    {
        if (reader.MoveNext(tokenType)) {
            return reader.GetString() ?? throw new InvalidDataException($"Encountered empty property name for token of type '{tokenType}'.");
        }
        throw new InvalidDataException($"Failed finding next token of type '{tokenType}'."); ;
    }

    public static Double ReadNumber(this ref Utf8JsonReader reader)
    {
        if (reader.MoveNext(Number)) {
            return reader.GetDouble();
        }
        throw new InvalidDataException("Failed finding next number");
    }

    public static String ReadString(this ref Utf8JsonReader reader)
    {
        if (reader.MoveNext(JsonTokenType.String)) {
            return reader.GetString() ?? throw new InvalidDataException("Expected a non null value for a string.");
        }
        throw new InvalidDataException("Failed finding next string");
    }

    public static void UnwindTo(this ref Utf8JsonReader reader, Int32 depth)
    {
        while (reader.CurrentDepth > depth && reader.Read()) { }
    }

    public static QuantityModel Read(this ref Utf8JsonReader reader, String system)
    {
        reader.MoveNext(StartObject);
        String? unit = null, prefix = null;
        while (unit == null) {
            var propertyName = reader.ReadNameOf(PropertyName);
            var propertyValue = reader.ReadString();
            if (propertyName is nameof(prefix)) {
                prefix = propertyValue;
                continue;
            }
            if (propertyName is nameof(unit)) {
                unit = propertyValue;
                continue;
            }
            throw new InvalidDataException($"Unknown property '{propertyName}' found on '{system}-system'.");
        }

        return new QuantityModel(system, prefix, unit);
    }
}
