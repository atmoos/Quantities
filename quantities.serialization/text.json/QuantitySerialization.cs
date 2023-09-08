using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quantities.Serialization.Text.Json;

internal sealed class QuantitySerialization : JsonConverterFactory
{
    private static readonly Type quantityConverter = typeof(QuantityConverter<>);
    public override Boolean CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsValueType && typeToConvert.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuantity<>));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type genericType = quantityConverter.MakeGenericType(typeToConvert);
        return Activator.CreateInstance(genericType) as JsonConverter;
    }
}
