using Newtonsoft.Json;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class FrequencySupportTest : ISerializationTester<Frequency>
{
    private static readonly JsonSerializerSettings options = new JsonSerializerSettings { Formatting = Formatting.Indented }.EnableQuantities();

    [Fact]
    public void HertzSerializesCorrectly()
    {
        const Double scalar = 2;
        Frequency frequency = Frequency.Of(scalar, Si<Hertz>());

        String actual = frequency.Serialize(options);

        String expected = $$"""
            {
              "value": {{scalar}}.0,
              "quantity": "frequency",
              "si": {
                "unit": "Hz"
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
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Frequency quantity) => quantity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
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
