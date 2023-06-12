using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Quantities.Measures;
using Quantities.Units.Si;

namespace Quantities.Serialization;

public sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private static readonly ConcurrentDictionary<String, Create> deserialization = new();
    public override TQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        const String unknown = nameof(unknown);
        String? type;
        if (reader.Read() && (type = reader.GetString()) != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? unknown}'. Expected: {name}.");
        }

        Int32 entries = 1;
        Double value = Double.NaN;
        String measure = unknown;
        String? token, unit = null, prefix = null;
        while (unit is null && reader.Read()) {
            if (reader.TokenType is JsonTokenType.StartObject) {
                entries++;
                continue;
            }
            if ((token = reader.GetString()) is null) {
                continue;
            }
            if (token is nameof(value) && reader.Read()) {
                value = reader.GetDouble();
                continue;
            }
            if (token is nameof(prefix) && reader.Read()) {
                prefix = reader.GetString();
                continue;
            }
            if (token is nameof(unit) && reader.Read()) {
                unit = reader.GetString();
                continue;
            }
            measure = token;
        }
        while (entries > 0 && reader.Read()) {
            if (reader.TokenType is JsonTokenType.EndObject) {
                entries--;
            }
        }
        return TQuantity.Create(Create(in value, measure, prefix, unit ?? unknown));
    }
    public override void Write(Utf8JsonWriter writer, TQuantity value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        var jsonWriter = new JsonWriter(writer);
        jsonWriter.Start(name);
        value.Serialize(jsonWriter);
        jsonWriter.End();
        writer.WriteEndObject();
    }

    private static Quant Create(in Double value, String measure, String? prefix, String unit)
    {
        var key = $"{measure}{prefix ?? String.Empty}{unit}"; // ToDo: Make this allocation free...
        if (!deserialization.TryGetValue(key, out var deserialize)) {
            deserialize = deserialization[key] = Deserialization.Find(measure, unit, prefix);
        }
        return deserialize(in value);
    }
}
