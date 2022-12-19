using System.Text.Json;
using Quantities.Serialization;

namespace Quantities.Test;

public class SerializationTest
{
    private static readonly JsonSerializerOptions options = Options();
    [Fact]
    public void Metric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value).Si<Metre>();
        String actual = Serialize(length);
        String expected = $$"""
        "length": {
          "value": {{value:R}},
          "metric": {
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void MetricWithPrefix()
    {
        Double value = Math.PI;
        Length length = Length.Of(value).Si<Kilo, Metre>();
        String actual = Serialize(length);
        String expected = $$"""
        "length": {
          "value": {{value:R}},
          "metric": {
            "prefix": "K",
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Imperial()
    {
        Double value = Math.PI;
        Length length = Length.Of(value).Imperial<Yard>();
        String actual = Serialize(length);
        String expected = $$"""
        "length": {
          "value": {{value:R}},
          "imperial": {
            "unit": "yd"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    private static String Serialize(Length length) => JsonSerializer.Serialize(length, options);

    private static JsonSerializerOptions Options()
    {
        var options = new JsonSerializerOptions {
            WriteIndented = true,
        };
        options.Converters.Add(new LengthSerialization());
        return options;
    }
}
