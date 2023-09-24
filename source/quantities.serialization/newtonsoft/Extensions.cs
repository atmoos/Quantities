using Newtonsoft.Json;

namespace Quantities.Serialization.Newtonsoft;

public static class Extensions
{
    public static JsonSerializerSettings EnableQuantities(this JsonSerializerSettings settings)
    {
        settings.Converters.Add(new QuantitySerialization());
        return settings;
    }
}
