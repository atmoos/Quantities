using System;
using System.Text.Json;

namespace Quantities.Benchmark;

public static class Convenience
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions().EnableQuantities();
    public static String Serialize<T>(this T value) => JsonSerializer.Serialize(value, options);
    public static T Deserialize<T>(this String value) => JsonSerializer.Deserialize<T>(value, options) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
}
