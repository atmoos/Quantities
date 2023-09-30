using System.Text.Json;

namespace Quantities.Serialization.Text.Json.Text;

public static class Convenience
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions().EnableQuantities(typeof(Gram).Assembly);
    public static T SupportsSerialization<T>(this T value)
        where T : IEquatable<T>
    {
        var deserialized = value.SerializeRoundRobin();

        Assert.Equal(value, deserialized);
        return deserialized;
    }
    public static T SerializeRoundRobin<T>(this T value) => Deserialize<T>(Serialize(value));
    public static String Serialize<T>(this T value) => Serialize(value, options);
    public static String Serialize<T>(this T value, JsonSerializerOptions options) => JsonSerializer.Serialize(value, options);
    public static T Deserialize<T>(this String value) => Deserialize<T>(value, options);
    public static T Deserialize<T>(this String value, JsonSerializerOptions options) => JsonSerializer.Deserialize<T>(value, options) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
}
