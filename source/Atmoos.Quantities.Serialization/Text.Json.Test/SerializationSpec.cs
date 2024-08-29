using System.Text.Json;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public sealed class SerializationSpec
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true }.EnableQuantities();

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
            "prefix": "K"
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
        Frequency frequency = value / Time.Of(1d, Si<Second>());
        String actual = frequency.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "frequency",
          "si": {
              "dimension": "time",
              "exponent": -1,
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
            "dimension": "length",
            "exponent": 2,
            "unit": "m"
          }
        }
        """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CubicMetricWithLitre()
    {
        Double value = Math.PI;
        Volume volume = Volume.Of(value, Metric<Litre>());
        String actual = volume.Serialize(options);
        String expected = $$"""
        {
          "value": {{value:R}},
          "quantity": "volume"
          "si": {
            "dimension": "length",
            "exponent": 3,
            "unit": "ℓ"
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
                "dimension": "length",
                "prefix": "k",
                "unit": "m"
              }
            },
            {
              "metric": {
                "dimension": "time",
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
