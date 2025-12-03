using System.Runtime.Serialization;
using Atmoos.Quantities.Core;
using Atmoos.Quantities.Core.Construction;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonToken;

namespace Atmoos.Quantities.Serialization.Newtonsoft;

file sealed class Writer : IWriter
{
    private readonly JsonWriter writer;
    public Writer(in JsonWriter writer) => this.writer = writer;
    public void Start() => this.writer.WriteStartObject();
    public void Start(String propertyName)
    {
        this.writer.WritePropertyName(propertyName);
        this.writer.WriteStartObject();
    }
    public void StartArray(String propertyName)
    {
        this.writer.WritePropertyName(propertyName);
        this.writer.WriteStartArray();
    }
    public void Write(String name, Double value)
    {
        this.writer.WritePropertyName(name);
        this.writer.WriteValue(value);
    }
    public void Write(String name, Int32 value)
    {
        this.writer.WritePropertyName(name);
        this.writer.WriteValue(value);
    }
    public void Write(String name, String value)
    {
        this.writer.WritePropertyName(name);
        this.writer.WriteValue(value);
    }
    public void EndArray() => this.writer.WriteEndArray();
    public void End() => this.writer.WriteEndObject();
}

internal sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private readonly UnitRepository repository;
    public QuantityConverter(UnitRepository repository) => this.repository = repository;
    public override TQuantity ReadJson(JsonReader reader, Type objectType, TQuantity existingValue, Boolean hasExistingValue, JsonSerializer serializer)
    {
        var initialDepth = reader.Depth;
        Double value = reader.ReadNumber();
        String type = reader.ReadString();
        if (type != name) {
            throw new SerializationException($"Cannot deserialize '{type ?? "unknown"}'. Expected: {name}.");
        }
        String system = reader.ReadNameOf(PropertyName);
        try {
            return (system == "measures" ? ReadMany(serializer, reader) : Read(serializer, reader, system)).Build(in value);
        }
        finally {
            reader.UnwindTo(initialDepth);
        }
    }

    public override void WriteJson(JsonWriter writer, TQuantity value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        value.Serialize(new Writer(writer));
        writer.WriteEndObject();
    }
    private QuantityFactory<TQuantity> ReadMany(JsonSerializer serializer, JsonReader reader)
    {
        reader.MoveNext(StartArray);
        var models = new List<QuantityModel>();
        while (reader.MoveNext(StartObject)) {
            models.Add(serializer.Read(reader, reader.ReadNameOf(PropertyName)));
            reader.MoveNext(EndObject);
        }
        reader.MoveNext(EndArray);
        return QuantityFactory<TQuantity>.Create(this.repository, models);
    }
    private QuantityFactory<TQuantity> Read(JsonSerializer serializer, JsonReader reader, String system)
    {
        var model = serializer.Read(reader, system);
        return QuantityFactory<TQuantity>.Create(this.repository, in model);
    }
}
