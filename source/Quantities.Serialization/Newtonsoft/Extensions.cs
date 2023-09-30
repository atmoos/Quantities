using System.Reflection;
using Newtonsoft.Json;

namespace Quantities.Serialization.Newtonsoft;

public static class Extensions
{
    public static JsonSerializerSettings EnableQuantities(this JsonSerializerSettings settings, params Assembly[] assemblies)
    {
        settings.Converters.Add(new QuantitySerialization(assemblies));
        return settings;
    }
}
