using System.Text.Json;

namespace Quantities.Serialization.Text.Json;

public static class Extensions
{
    public static JsonSerializerOptions EnableQuantities(this JsonSerializerOptions options)
    {
        options.Converters.Add(new QuantitySerialization());
        return options;
    }
}
