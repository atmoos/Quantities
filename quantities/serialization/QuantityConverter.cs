using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Quantities.Measures;
using Quantities.serialization;

using static System.Text.Json.JsonTokenType;

namespace Quantities.Serialization;

public sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private static readonly ConcurrentDictionary<String, CreateStore> deserialization = new();
    public override TQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var initialDepth = reader.CurrentDepth;
        String type = reader.ReadNameOf(PropertyName);
        if (type != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? "unknown"}'. Expected: {name}.");
        }
        Double value = reader.ReadNumber();
        String system = reader.ReadNameOf(PropertyName);
        QuantityModel model = reader.Read(system);
        reader.UnwindTo(initialDepth);
        return TQuantity.Create(Create(in value, model.System, model.Prefix, model.Unit));
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
        if (!deserialization.TryGetValue(measure, out var store)) {
            store = deserialization[measure] = new CreateStore();
        }
        return (store[prefix, unit] ??= Deserialization.Find(measure, prefix, unit))(in value);
    }
}

internal sealed class CreateStore
{
    private const String noPrefix = "none";
    // prefix -> unit
    private readonly Dictionary<String, Dictionary<String, Create>> creators = new();
    public Create? this[String? prefix, String unit]
    {
        get => this.creators.TryGetValue(prefix ?? noPrefix, out var units) && units.TryGetValue(unit, out var create) ? create : null;
        set {
            if (value is null) {
                return;
            }
            prefix ??= noPrefix;
            if (!this.creators.TryGetValue(prefix, out var units)) {
                units = this.creators[prefix] = new Dictionary<String, Create>();
            }
            units[unit] = value;
        }
    }
}