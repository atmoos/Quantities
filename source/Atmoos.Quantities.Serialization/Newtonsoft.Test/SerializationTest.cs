using Atmoos.Quantities.Physics;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;
using Newtonsoft.Json;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

public class SerializationTest
{
    private static readonly JsonSerializerSettings options = new JsonSerializerSettings { Formatting = Formatting.Indented }.EnableQuantities();

    [Fact]
    public void FalseUnitsCannotBeInjectedViaSerialization()
    {
        Length length = Length.Of(6, Si<Metre>());

        String falseUnit = length.Serialize().Replace(Metre.Representation, Ohm.Representation);

        var msg = Assert.Throws<InvalidOperationException>(() => falseUnit.Deserialize<Length>()).Message;
        Assert.StartsWith("Cannot build a quantity of type", msg);
        Assert.Contains(nameof(Length), msg);
    }

    [Fact]
    public void AliasingUnitsAreSupported()
    {
        Volume fourCubicMetersInLitres = Volume.Of(4000, Metric<Litre>());
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
    public void PowerRepresentationsSupported()
    {
        Volume volume = Volume.Of(2, Cubic(Si<Metre>()));

        Volume roundRobinSerialization = volume.SerializeRoundRobin();

        Assert.Equal(volume.ToString(), roundRobinSerialization.ToString());
    }

    [Fact]
    public void Complex()
    {
        var person = new Person {
            Name = "Foo Bar",
            Height = Length.Of(1.67, Si<Metre>()),
            Weight = Mass.Of(72, Si<Kilogram>()),
        };
        String actual = person.Serialize(options);
        String expected = """
            {
              "Name": "Foo Bar",
              "Height": {
                "value": 1.67,
                "quantity": "length",
                "si": {
                  "unit": "m"
                }
              },
              "Weight": {
                "value": 72.0,
                "quantity": "mass",
                "si": {
                  "unit": "kg"
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
            Weight = Mass.Of(68, Si<Kilogram>()),
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
