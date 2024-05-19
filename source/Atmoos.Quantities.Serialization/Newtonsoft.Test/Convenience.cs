using Newtonsoft.Json;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

public static class Convenience
{
    private static readonly JsonSerializerSettings options = new JsonSerializerSettings().EnableQuantities(typeof(Gram).Assembly);

    public static T SupportsSerialization<T>(this T value)
        where T : IEquatable<T>
    {
        var serialized = value.Serialize();
        var deserialized = Deserialize<T>(serialized);

        Assert.Equal(value, deserialized);
        return deserialized;
    }
    public static T SerializeRoundRobin<T>(this T value) => Deserialize<T>(value.Serialize());
    public static String Serialize<T>(this T value) => Serialize(value, options);
    public static String Serialize<T>(this T value, JsonSerializerSettings options) => JsonConvert.SerializeObject(value, options);
    public static T Deserialize<T>(String value) => Deserialize<T>(value, options);
    public static T Deserialize<T>(String value, JsonSerializerSettings options) => JsonConvert.DeserializeObject<T>(value, options) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
}
