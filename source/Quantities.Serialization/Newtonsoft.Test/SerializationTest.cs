using Newtonsoft.Json;
using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Si;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

using static Quantities.Serialization.Newtonsoft.Test.Convenience;

namespace Quantities.Serialization.Newtonsoft.Test;

public class SerializationTest
{
    private static readonly JsonSerializerSettings options = new JsonSerializerSettings { Formatting = Formatting.Indented }.EnableQuantities(typeof(Gram).Assembly);

    [Fact]
    public void FalseUnitsCannotBeInjectedViaSerialization()
    {
        Length length = Length.Of(6, Si<Metre>());

        String falseUnit = length.Serialize().Replace(Metre.Representation, Ohm.Representation);

        var msg = Assert.Throws<InvalidOperationException>(() => Deserialize<Length>(falseUnit)).Message;
        Assert.StartsWith("Dimension mismatch:", msg);
        Assert.Contains(nameof(Ohm), msg);
        Assert.Contains(nameof(ILength), msg);
        Assert.Contains(nameof(IElectricalResistance), msg);
    }

    [Fact]
    public void AliasingUnitsAreSupported()
    {
        Volume fourCubicMetersInLitres = Volume.Of(4000, AliasOf<ILength>.Metric<Litre>());
        Area oneSquareMetre = Area.Of(1, Square(Si<Metre>()));
        Length expectedHeight = fourCubicMetersInLitres / oneSquareMetre;

        Volume deserializedVolume = fourCubicMetersInLitres.SerializeRoundRobin();
        Length actualHeight = deserializedVolume / oneSquareMetre;

        Assert.Equal(expectedHeight, actualHeight);
    }

    [Fact]
    public void ReadMetric()
    {
        Double value = Math.PI;
        Length expected = Length.Of(value, Si<Metre>());

        expected.SupportsSerialization();
    }

    [Fact]
    public void ReadPrefixedMetric()
    {
        Double value = Math.PI;
        Length expected = Length.Of(value, Si<Centi, Metre>());

        expected.SupportsSerialization();
    }

    [Fact]
    public void ReadImperial()
    {
        Double value = Math.E;
        Length expected = Length.Of(value, Imperial<Mile>());

        expected.SupportsSerialization();
    }

    [Fact]
    public void Metric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value, Si<Metre>());
        String actual = length.Serialize(options);
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
        Length length = Length.Of(value, Si<Kilo, Metre>());
        String actual = length.Serialize(options);
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
        Length length = Length.Of(value, Imperial<Yard>());
        String actual = length.Serialize(options);
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
    public void PowerRepresentationsSupported()
    {
        Volume volume = Volume.Of(2, Cubic(Si<Metre>()));

        Volume roundRobinSerialization = volume.SerializeRoundRobin();

        Assert.Equal(volume.ToString(), roundRobinSerialization.ToString());
    }

    [Fact]
    public void Compound()
    {
        Double value = Math.PI;
        Velocity velocity = Velocity.Of(value, Si<Kilo, Metre>().Per(Metric<Hour>()));
        String actual = velocity.Serialize(options);
        String expected = $$"""
        {
          "velocity": {
            "value": {{value:R}},
            "quotient": {
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
            Height = Length.Of(1.67, Si<Metre>()),
            Weight = Mass.Of(72, Si<Kilogram>())
        };
        String actual = person.Serialize(options);
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
              "value": 72.0,
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
            Height = Length.Of(16.7, Si<Deci, Metre>()),
            Weight = Mass.Of(68, Si<Kilogram>())
        };

        Person actual = expected.SerializeRoundRobin();

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
