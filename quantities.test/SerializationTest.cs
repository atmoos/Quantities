using System.Text.Json;
using Quantities.Serialization;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public class SerializationTest
{
    private static readonly JsonSerializerOptions options = Options();

    [Fact]
    public void ReadMetric()
    {
        Double value = Math.PI;
        Length expected = Length.Of(value).Si<Centi, Metre>();
        String data = Serialize(expected);

        Length actual = Deserialize<Length>(data);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Metric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value).Si<Metre>();
        String actual = Serialize(length);
        String expected = $$"""
        {
          "length": {
            "value": {{value:R}},
            "si": {
              "unit": "m"
            }
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
        {
          "length": {
            "value": {{value:R}},
            "si": {
              "prefix": "K",
              "unit": "m"
            }
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
        {
          "length": {
            "value": {{value:R}},
            "imperial": {
              "unit": "yd"
            }
          }
        }
        """;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Compound()
    {
        Double value = Math.PI;
        Velocity velocity = Velocity.Of(value).Si<Kilo, Metre>().Per.Metric<Hour>();
        String actual = Serialize(velocity);
        String expected = $$"""
        {
          "velocity": {
            "value": {{value:R}},
            "frac": {
              "si": {
                "prefix": "K",
                "unit": "m"
              },
              "metric": {
                "unit": "h"
              }
            }
          }
        }
        """;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Complex()
    {
        var person = new Person {
            Name = "Foo Bar",
            Height = Length.Of(1.67).Si<Metre>(),
            Weight = Mass.Of(72).Si<Kilogram>()
        };
        String actual = Serialize(person);
        String expected = """
        {
          "Name": "Foo Bar",
          "Height": {
            "length": {
              "value": 1.67,
              "si": {
                "unit": "m"
              }
            }
          },
          "Weight": {
            "mass": {
              "value": 72,
              "si": {
                "unit": "kg"
              }
            }
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    private static String Serialize<T>(T value) => JsonSerializer.Serialize(value, options);
    private static T? Deserialize<T>(String value) => JsonSerializer.Deserialize<T>(value, options);

    private static JsonSerializerOptions Options()
    {
        var options = new JsonSerializerOptions {
            WriteIndented = true
        };
        options.Converters.Add(new QuantitySerialization());
        return options;
    }

    private sealed class Person
    {
        public required String Name { get; init; }
        public required Length Height { get; init; }
        public required Mass Weight { get; init; }
    }
}
