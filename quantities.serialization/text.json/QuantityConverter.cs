using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Text.Json.JsonTokenType;

namespace Quantities.Serialization.Text.Json;

file sealed class JsonWriter : IWriter
{
    private readonly Utf8JsonWriter writer;
    public JsonWriter(in Utf8JsonWriter writer) => this.writer = writer;
    public void Start(String propertyName) => this.writer.WriteStartObject(propertyName);
    public void Write(String name, Double value) => this.writer.WriteNumber(name, value);
    public void Write(String name, String value) => this.writer.WriteString(name, value);
    public void End() => this.writer.WriteEndObject();
}

internal sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    public override TQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var initialDepth = reader.CurrentDepth;
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
    public override void Write(Utf8JsonWriter writer, TQuantity value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        value.Serialize(new JsonWriter(writer));
        writer.WriteEndObject();
    }
}
