using Quantities.Units.Imperial.Length;
using Quantities.Units.NonStandard.Length;

namespace Quantities.Serialization.Text.Json.Test;

public class LengthSupportTest : ISerializationTester<Length>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Length quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Length> Interesting()
        {
            yield return Length.Of(21, Si<Metre>());
            yield return Length.Of(342, Si<Micro, Metre>());
            yield return Length.Of(1, Si<Mega, Metre>());
            yield return Length.Of(-11, Imperial<Mile>());
            yield return Length.Of(9, Imperial<Foot>());
            yield return Length.Of(54, Imperial<Yard>());
            yield return Length.Of(3.2, Metric<Kilo, Ångström>());
            yield return Length.Of(11, NonStandard<LightYear>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
