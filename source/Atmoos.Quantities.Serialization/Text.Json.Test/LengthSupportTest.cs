using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.NonStandard.Length;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class LengthSupportTest : ISerializationTester<Length>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Length quantity) => quantity.SupportsSerialization();

    public static TheoryData<Length> Quantities() => [
            Length.Of(21, Si<Metre>()),
            Length.Of(342, Si<Micro, Metre>()),
            Length.Of(1, Si<Mega, Metre>()),
            Length.Of(-11, Imperial<Mile>()),
            Length.Of(9, Imperial<Foot>()),
            Length.Of(54, Imperial<Yard>()),
            Length.Of(3.2, Metric<Kilo, Ångström>()),
            Length.Of(11, NonStandard<LightYear>()),
        ];
}
