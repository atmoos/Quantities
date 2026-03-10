using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Measures;

namespace Atmoos.Quantities.Test.Serialization;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class SerializerTest
{
    [Fact]
    public void ExponentOneOmitsExponentField()
    {
        var writer = new SpyWriter();
        var serializer = new Serializer<Metre>("Si");

        serializer.Write(writer, 1);

        Assert.Equal(["start:si", "unit:m", "end"], writer.Events);
    }

    [Fact]
    public void ExponentOtherThanOneWritesExponentField()
    {
        var writer = new SpyWriter();
        var serializer = new Serializer<Metre>("Si");

        serializer.Write(writer, 2);

        Assert.Equal(["start:si", "exponent:2", "unit:m", "end"], writer.Events);
    }

    [Fact]
    public void PrefixedSerializationWritesPrefixBetweenExponentAndUnit()
    {
        var writer = new SpyWriter();
        var serializer = new Serializer<Metre>("Si");

        serializer.Write<Kilo>(writer, 3);

        Assert.Equal(["start:si", "exponent:3", "prefix:k", "unit:m", "end"], writer.Events);
    }

    [Fact]
    public void SystemNameIsNormalizedToLowerInvariant()
    {
        var writer = new SpyWriter();
        var serializer = new Serializer<Metre>("MiXeD");

        serializer.Write(writer, 1);

        Assert.Equal("start:mixed", writer.Events[0]);
    }

    private sealed class SpyWriter : IWriter
    {
        private readonly List<String> events = [];
        public IReadOnlyList<String> Events => this.events;

        public void Start() => this.events.Add("start");

        public void Start(String propertyName) => this.events.Add($"start:{propertyName}");

        public void StartArray(String propertyName) => this.events.Add($"start-array:{propertyName}");

        public void Write(String name, Double value) => this.events.Add($"{name}:{value:g17}");

        public void Write(String name, Int32 value) => this.events.Add($"{name}:{value}");

        public void Write(String name, String value) => this.events.Add($"{name}:{value}");

        public void EndArray() => this.events.Add("end-array");

        public void End() => this.events.Add("end");
    }
}
