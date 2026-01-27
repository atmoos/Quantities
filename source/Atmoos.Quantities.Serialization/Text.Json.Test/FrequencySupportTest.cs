using System.Text.Json;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class FrequencySupportTest : IInjectedUnitTester<Frequency>
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true }.EnableQuantities();

    [Fact]
    public void HertzDeserializesCorrectly()
    {
        const Double scalar = 2;
        Frequency frequency = Frequency.Of(scalar, Si<Hertz>());

        String actual = frequency.Serialize(options);

        String expected = $$"""
            {
              "value": {{scalar:R}},
              "quantity": "frequency",
              "si": {
                "unit": "Hz"
              }
            }
            """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InverseTimeDeserializesCorrectly()
    {
        Time time = Time.Of(0.5, Si<Second>());
        Frequency frequency = 2d / time;

        String actual = frequency.Serialize(options);

        String expected = $$"""
            {
              "value": 4,
              "quantity": "frequency",
              "si": {
                "exponent": -1,
                "unit": "s"
              }
            }
            """;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InverseTimeSerializesRoundRobin()
    {
        Time time = Time.Of(1, Si<Second>());
        Frequency expected = 6d / time;

        Frequency roundRobin = expected.SerializeRoundRobin();

        Assert.Equal(expected, roundRobin);
    }

    [Theory]
    [MemberData(nameof(InjectingFrequencies))]
    public void DeserializationSupportsInjectedUnits(Frequency quantity)
    {
        const Double scalar = 2;
        Time expectedTime = scalar / quantity;

        Frequency deserializedFrequency = quantity.SupportsSerialization();
        Time actualTimeFromDeserializedFrequency = scalar / deserializedFrequency;

        Assert.Equal(expectedTime.ToString(), actualTimeFromDeserializedFrequency.ToString());
    }

    public static IEnumerable<Object[]> InjectingFrequencies()
    {
        static IEnumerable<Frequency> Interesting()
        {
            yield return Frequency.Of(2, Si<Hertz>());
            yield return Frequency.Of(-1, Si<Centi, Hertz>());
            yield return Frequency.Of(3, Si<Kilo, Hertz>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
