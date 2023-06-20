using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Quantities.Dimensions;
using Quantities.Measures;
using static System.Text.Json.JsonTokenType;

namespace Quantities.Serialization;

public sealed class QuantityConverter<TQuantity> : JsonConverter<TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension, IFactory<TQuantity>
{
    private static readonly String name = typeof(TQuantity).Name.ToLowerInvariant();
    private static readonly ITypeVerification scalarVerification = new ScalarVerification(typeof(TQuantity).MostDerivedOf<Dimensions.IDimension>());
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
        var (inject, builder) = Create(system, ref reader);
        reader.UnwindTo(initialDepth);
        return TQuantity.Create(builder.Append(inject).Build(in value));
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

    private static (IInject, IBuilder) Create(String system, ref Utf8JsonReader reader) => system switch {
        "frac" => (new FractionInjector(), AggregateOf(ref reader, 2)),
        "prod" => (new ProductInjector(), AggregateOf(ref reader, 2)),
        "square" => (new PowerInjector<Square>(), Power(ref reader, new ScalarVerification(typeof(TQuantity).InnerTypes(typeof(ISquare<>)).Single()))),
        "cubic" => (new PowerInjector<Cube>(), Power(ref reader, new ScalarVerification(typeof(TQuantity).InnerTypes(typeof(ICubic<>)).Single()))),
        _ => (new ScalarInjector(), Linear(reader.Read(system)))
    };

    private static IBuilder Power(ref Utf8JsonReader reader, ITypeVerification verification)
    {
        reader.MoveNext(StartObject);
        return Linear(reader.Read(reader.ReadNameOf(PropertyName)), verification);
    }

    private static IBuilder Linear(in QuantityModel model) => Linear(in model, scalarVerification);
    private static IBuilder Linear(in QuantityModel model, ITypeVerification verification)
    {
        if (builders.TryGetValue(model, out var builder)) {
            return builder;
        }
        return builders[model] = ScalarBuilder.Create(model, verification);
    }

    private static IBuilder AggregateOf(ref Utf8JsonReader reader, Int32 count)
    {
        reader.MoveNext(StartObject);
        var builders = new List<IBuilder>(count);
        while (builders.Count < count) {
            builders.Add(Linear(reader.Read(reader.ReadNameOf(PropertyName))));
        }
        return new AggregateBuilder(builders);
    }
}
