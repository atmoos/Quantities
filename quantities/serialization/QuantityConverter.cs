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
    public override TQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        const String unknown = nameof(unknown);
        String? type;
        if (reader.Read() && (type = reader.GetString()) != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? unknown}'. Expected: {name}.");
        }

        Double value = Double.NaN;
        String? token, prefix = null;
        String measure = unknown, unit = unknown;
        while (reader.Read()) {
            if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.EndObject) {
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
                unit = reader.GetString() ?? unknown;
                continue;
            }
            measure = token;
        }
        return TQuantity.Create(Create(in value, measure, prefix, unit));
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
        return Build<Si<Metre>>.With(in value);
    }
}
