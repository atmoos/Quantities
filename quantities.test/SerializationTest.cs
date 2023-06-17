using System.Text.Json;
using Quantities.Units.Si.Metric;

namespace Quantities.Test;

public class SerializationTest
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true }.EnableQuantities();

    [Fact]
    public void ReadMetric()
    {
        Double value = Math.PI;
        Length expected = Length.Of(value).Si<Metre>();

        expected.CanBeSerialized();
    }

    [Fact]
    public void ReadPrefixedMetric()
    {
        Double value = Math.PI;
        Length expected = Length.Of(value).Si<Centi, Metre>();

        expected.CanBeSerialized();
    }

    [Fact]
    public void ReadImperial()
    {
        Double value = Math.E;
        Length expected = Length.Of(value).Imperial<Mile>();

        expected.CanBeSerialized();
    }

    [Fact]
    public void Metric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value).Si<Metre>();
        String actual = Serialize(length, options);
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
        String actual = Serialize(length, options);
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
        String actual = Serialize(length, options);
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
        String actual = Serialize(velocity, options);
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
        String actual = Serialize(person, options);
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

    [Fact]
    public void DeserializeComplex()
    {
        var expected = new Person {
            Name = "Hello Deserialization!",
            Height = Length.Of(16.7).Si<Deci, Metre>(),
            Weight = Mass.Of(68).Si<Kilogram>()
        };
        String data = Serialize(expected);

        Person actual = Deserialize<Person>(data);

        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Height, actual.Height);
        Assert.Equal(expected.Weight, actual.Weight);
    }

    private sealed class Person
    {
        public required String Name { get; init; }
        public required Length Height { get; init; }
        public required Mass Weight { get; init; }
    }
}
