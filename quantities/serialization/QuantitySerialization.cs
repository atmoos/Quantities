using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quantities.Serialization;

public sealed class QuantitySerialization : JsonConverterFactory
{
    public override Boolean CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsValueType && typeToConvert.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuantity<>));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type genericType = typeof(QuantityConverter<>).MakeGenericType(new Type[] { typeToConvert });
        return Activator.CreateInstance(genericType) as JsonConverter;
    }
}
