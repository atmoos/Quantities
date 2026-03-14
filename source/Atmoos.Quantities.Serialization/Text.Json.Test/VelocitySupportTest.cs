using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.NonStandard.Length;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class VelocitySupportTest : ISerializationTester<Velocity>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Velocity velocity) => velocity.SupportsSerialization();

    public static TheoryData<Velocity> Quantities() => [
            Velocity.Of(21, Si<Metre>().Per(Si<Second>())),
            Velocity.Of(342, Si<Kilo, Metre>().Per(Metric<Hour>())),
            Velocity.Of(6, Imperial<Mile>().Per(Metric<Hour>())),
            Velocity.Of(-41, Imperial<Foot>().Per(Si<Second>())),
            Velocity.Of(1.21, Metric<Ångström>().Per(Si<Micro, Second>())),
            Velocity.Of(0.125, Metric<AstronomicalUnit>().Per(Metric<Minute>())),
            Velocity.Of(0.000292002383, NonStandard<LightYear>().Per(Metric<Week>())),
        ];
}
