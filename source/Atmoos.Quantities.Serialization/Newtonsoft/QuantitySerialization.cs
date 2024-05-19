using System.Reflection;
using Newtonsoft.Json;

namespace Atmoos.Quantities.Serialization.Newtonsoft;

internal sealed class QuantitySerialization : JsonConverter
{
    private static readonly Type quantityConverter = typeof(QuantityConverter<>);
    private readonly Dictionary<Type, JsonConverter> converters = new();
    private readonly UnitRepository repository;
    public QuantitySerialization(Assembly[] assemblies) => this.repository = UnitRepository.Create(assemblies);
    public override Boolean CanConvert(Type objectType)
    {
        if (this.converters.ContainsKey(objectType)) {
            return true;
        }
        if (objectType.IsValueType && objectType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuantity<>))) {
            Type genericType = quantityConverter.MakeGenericType(objectType);
            if (Activator.CreateInstance(genericType, new Object[] { this.repository }) is JsonConverter converter) {
                this.converters[objectType] = converter;
                return true;
            }
        }
        return false;
    }

    public override Object? ReadJson(JsonReader reader, Type objectType, Object? existingValue, JsonSerializer serializer)
    {
        if (this.converters.TryGetValue(objectType, out var converter)) {
            return converter.ReadJson(reader, objectType, existingValue, serializer);
        }
        return null;
    }
    public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer)
    {
        var type = value?.GetType();
        if (type != null && this.converters.TryGetValue(type, out var converter)) {
            converter.WriteJson(writer, value, serializer);
        }
    }
}
