using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Quantities.Measures;

using static System.Text.Json.JsonTokenType;

namespace Quantities.Serialization;

public sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private static readonly ConcurrentDictionary<QuantityModel, IBuilder> builders = new();
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
        return TQuantity.Create(Create(in model).Build(in value));
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

    private static IBuild Create(in QuantityModel model)
    {
        if (builders.TryGetValue(model, out var builder)) {
            return builder;
        }
        return builders[model] = new ScalarBuilder(model).Append(new ScalarInjector());
    }
}
