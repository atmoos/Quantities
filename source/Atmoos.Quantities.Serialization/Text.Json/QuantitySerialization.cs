using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Atmoos.Quantities.Serialization.Text.Json;

internal sealed class QuantitySerialization : JsonConverterFactory
{
    private readonly UnitRepository repository;
    private static readonly Type quantityConverter = typeof(QuantityConverter<>);
    public QuantitySerialization(Assembly[] assemblies) => this.repository = UnitRepository.Create(assemblies);
    public override Boolean CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsValueType && typeToConvert.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuantity<>));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type genericType = quantityConverter.MakeGenericType(typeToConvert);
        return Activator.CreateInstance(genericType, [this.repository]) as JsonConverter;
    }
}
