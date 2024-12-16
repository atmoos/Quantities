using System.Text.Json;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.Imperial.Volume;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public sealed class SerializationSpec
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true }.EnableQuantities();

    [Fact]
    public void Identity()
    {
        throw new NotImplementedException("ToDo...");
    }

    [Fact]
    public void LinearMetric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value, Si<Metre>());
        String actual = length.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "length",
          "si": {
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void LinearPrefixedMetric()
    {
        Double value = Math.PI;
        Length length = Length.Of(value, Si<Kilo, Metre>());
        String actual = length.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "length",
          "si": {
            "prefix": "k",
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InverseMetric()
    {
        Double value = Math.PI;
        Frequency frequency = value / Time.Of(1d, Si<Milli, Second>());
        String actual = frequency.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "frequency",
          "si": {
            "exponent": -1,
            "prefix": "m",
            "unit": "s"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SquareMetric()
    {
        Double value = Math.PI;
        Area area = Area.Of(value, Square(Si<Metre>()));
        String actual = area.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "area",
          "si": {
            "exponent": 2,
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CubicImperial()
    {
        Double value = Math.PI;
        Volume volume = Volume.Of(value, Cubic(Imperial<Foot>()));
        String actual = volume.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "volume",
          "imperial": {
            "exponent": 3,
            "unit": "ft"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CubicWithImperialPint()
    {
        Double value = Math.PI;
        Volume volume = Volume.Of(value, Imperial<Pint>());
        String actual = volume.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "volume",
          "imperial": {
            "unit": "pt"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CubicWithMetricLitre()
    {
        Double value = Math.PI;
        Volume volume = Volume.Of(value, Metric<Hecto, Litre>());
        String actual = volume.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "volume",
          "metric": {
            "prefix": "h",
            "unit": "\u2113"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CompoundVelocity()
    {
        Double value = Math.PI;
        Velocity velocity = Velocity.Of(value, Si<Kilo, Metre>().Per(Metric<Hour>()));
        String actual = velocity.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "velocity",
          "measures": [
            {
              "si": {
                "prefix": "k",
                "unit": "m"
              }
            },
            {
              "metric": {
                "exponent": -1,
                "unit": "h"
              }
            }
          ]
        }
        """;
        Assert.Equal(expected, actual);
    }
}
