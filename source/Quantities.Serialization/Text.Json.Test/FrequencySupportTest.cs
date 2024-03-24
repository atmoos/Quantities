namespace Quantities.Serialization.Text.Json.Test;

public class FrequencySupportTest : IInjectedUnitTester<Frequency>
{

    [Theory]
    [MemberData(nameof(InjectingAreas))]
    public void DeserializationSupportsInjectedUnits(Frequency quantity)
    {
        const Double scalar = 2;
        Time expectedTime = scalar / quantity;
        Frequency deserializedFrequency = quantity.SupportsSerialization();

        Time actualTimeFromDeserializedFrequency = scalar / deserializedFrequency;

        Assert.Equal(expectedTime.ToString(), actualTimeFromDeserializedFrequency.ToString());
    }

    public static IEnumerable<Object[]> InjectingAreas()
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
