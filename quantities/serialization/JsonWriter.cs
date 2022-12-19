using System.Text.Json;

namespace Quantities.Serialization;

public sealed class JsonWriter : IWriter
{
    private readonly Utf8JsonWriter writer;
    public JsonWriter(Utf8JsonWriter writer) => this.writer = writer;
    public void Start(String propertyName) => this.writer.WriteStartObject(propertyName);
    public void Write(String name, Double value) => this.writer.WriteNumber(name, value);
    public void Write(String name, String value) => this.writer.WriteString(name, value);
    public void End() => this.writer.WriteEndObject();
}
