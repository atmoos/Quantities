using Atmoos.Quantities.Core.Construction;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonToken;

namespace Atmoos.Quantities.Serialization.Newtonsoft;

file record struct Measure(Int32? exponent, String? prefix, String unit);

internal static class Deserializer
{
    public static Boolean MoveNext(this JsonReader reader, JsonToken tokenType)
    {
        Boolean readNext = true;
        while (reader.TokenType != tokenType && (readNext = reader.Read())) { }
        return readNext;
    }

    public static String ReadNameOf(this JsonReader reader, JsonToken tokenType)
    {
        if (reader.MoveNext(tokenType)) {
            return reader.Value as String ?? throw new InvalidDataException($"Encountered empty property name for token of type '{tokenType}'.");
        }
        throw new InvalidDataException($"Failed finding next token of type '{tokenType}'."); ;
    }

    public static Double ReadNumber(this JsonReader reader)
    {
        if (reader.MoveNext(Float)) {
            return reader.Value is Double value ? value : throw new InvalidDataException("Expected a non null value for a Double.");
        }
        throw new InvalidDataException("Failed finding next number");
    }

    public static String ReadString(this JsonReader reader)
    {
        if (reader.MoveNext(JsonToken.String)) {
            return reader.Value as String ?? throw new InvalidDataException("Expected a non null value for a string.");
        }
        throw new InvalidDataException("Failed finding next string");
    }

    public static void UnwindTo(this JsonReader reader, Int32 depth)
    {
        while (reader.Depth > depth && reader.Read()) { }
    }

    public static QuantityModel Read(this JsonSerializer serializer, JsonReader reader, String system)
    {
        reader.MoveNext(StartObject);
        Measure measure = serializer.Deserialize<Measure>(reader);
        return new QuantityModel {
            System = system,
            Exponent = measure.exponent ?? 1,
            Prefix = measure.prefix,
            Unit = measure.unit
        };
    }
}
