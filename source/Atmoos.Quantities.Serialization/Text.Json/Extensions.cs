using System.Reflection;
using System.Text.Json;

namespace Atmoos.Quantities.Serialization.Text.Json;

public static class Extensions
{
    public static JsonSerializerOptions EnableQuantities(this JsonSerializerOptions options, params Assembly[] assemblies)
    {
        options.Converters.Add(new QuantitySerialization(assemblies));
        return options;
    }
}
