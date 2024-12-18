using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Atmoos.Quantities.Core;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using static System.Text.Json.JsonTokenType;

namespace Atmoos.Quantities.Serialization.Text.Json;

file sealed class JsonWriter : IWriter
{
    private readonly Utf8JsonWriter writer;
    public JsonWriter(in Utf8JsonWriter writer) => this.writer = writer;
    public void Start() => this.writer.WriteStartObject();
    public void Start(String propertyName) => this.writer.WriteStartObject(propertyName);
    public void StartArray(String propertyName) => this.writer.WriteStartArray(propertyName);
    public void Write(String name, Double value) => this.writer.WriteNumber(name, value);
    public void Write(String name, String value) => this.writer.WriteString(name, value);
    public void EndArray() => this.writer.WriteEndArray();
    public void End() => this.writer.WriteEndObject();
}

internal sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private readonly UnitRepository repository;
    public QuantityConverter(UnitRepository repository) => this.repository = repository;
    public override TQuantity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var initialDepth = reader.CurrentDepth;
        Double value = reader.ReadNumber();
        String type = reader.ReadString();
        if (type != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? "unknown"}'. Expected: {name}.");
        }
        String system = reader.ReadNameOf(PropertyName);
        try {
            return (system == "measures" ? ReadMany(ref reader) : Read(ref reader, system)).Build(in value);
        }
        finally {
            reader.UnwindTo(initialDepth);
        }
    }
    public override void Write(Utf8JsonWriter writer, TQuantity value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        value.Serialize(new JsonWriter(writer));
        writer.WriteEndObject();
    }
    private QuantityFactory<TQuantity> ReadMany(ref Utf8JsonReader reader)
    {
        reader.MoveNext(StartArray);
        var models = new List<QuantityModel>();
        while (reader.MoveNext(StartObject)) {
            models.Add(reader.Read(reader.ReadNameOf(PropertyName)));
            reader.MoveNext(EndObject);
        }
        reader.MoveNext(EndArray);
        return QuantityFactory<TQuantity>.Create(this.repository, models);
    }
    private QuantityFactory<TQuantity> Read(ref Utf8JsonReader reader, String system)
    {
        var model = reader.Read(system);
        return QuantityFactory<TQuantity>.Create(this.repository, in model);
    }
}
