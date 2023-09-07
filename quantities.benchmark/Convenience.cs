using System.Text.Json;
using Newtonsoft.Json;
using Quantities.Numerics;
using Quantities.Serialization.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Quantities.Benchmark;

internal static class Convenience
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions().EnableQuantities();
    public static String Serialize<T>(this T value) => JsonSerializer.Serialize(value, options);
    public static T Deserialize<T>(this String value) => JsonSerializer.Deserialize<T>(value, options) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
    public static T Deserialize<T>(this String value, JsonSerializerSettings settings) => JsonConvert.DeserializeObject<T>(value, settings) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
    public static Polynomial Poly(in Double nominator = 1, in Double denominator = 1, in Double offset = 0)
    {
        var value = new Transformation();
        return Polynomial.Of(nominator * value / denominator + offset);
    }
}
