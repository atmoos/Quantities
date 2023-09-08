using System.Runtime.Serialization;
using Newtonsoft.Json;

using static Newtonsoft.Json.JsonToken;

namespace Quantities.Serialization.Newtonsoft;

file sealed class Writer : IWriter
{
    private readonly JsonWriter writer;
    public Writer(in JsonWriter writer) => this.writer = writer;
    public void Start(String propertyName)
    {
        this.writer.WritePropertyName(propertyName);
        this.writer.WriteStartObject();
    }
    public void Write(String name, Double value)
    {
        this.writer.WritePropertyName(name);
        this.writer.WriteValue(value);
    }
    public void Write(String name, String value)
    {
        this.writer.WritePropertyName(name);
        this.writer.WriteValue(value);
    }
    public void End() => this.writer.WriteEndObject();
}

internal sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();

    public override TQuantity ReadJson(JsonReader reader, Type objectType, TQuantity existingValue, Boolean hasExistingValue, JsonSerializer serializer)
    {
        Int32 initialDepth = reader.Depth;
        String type = reader.ReadNameOf(PropertyName);
        if (type != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? "unknown"}'. Expected: {name}.");
        }
        Double value = reader.ReadNumber();
        String system = reader.ReadNameOf(PropertyName);
        QuantityFactory<TQuantity> factory = QuantityFactory<TQuantity>.Create(system);
        if (factory.ExpectedModelCount == 1) {
            if (!factory.IsScalarQuantity) {
                reader.MoveNext(StartObject);
                system = reader.ReadNameOf(PropertyName);
            }
            var model = reader.Read(system);
            reader.UnwindTo(initialDepth);
            return factory.Build(in value, in model);
        }
        reader.MoveNext(StartObject);
        var models = new QuantityModel[factory.ExpectedModelCount];
        for (var modelNumber = 0; modelNumber < models.Length; ++modelNumber) {
            models[modelNumber] = reader.Read(reader.ReadNameOf(PropertyName));
        }
        reader.UnwindTo(initialDepth);
        return factory.Build(in value, models);
    }

    public override void WriteJson(JsonWriter writer, TQuantity value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        value.Serialize(new Writer(writer));
        writer.WriteEndObject();
    }
}
