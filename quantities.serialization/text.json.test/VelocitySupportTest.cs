using Quantities.Units.Imperial.Length;
using Quantities.Units.NonStandard.Length;

namespace Quantities.Serialization.Text.Json.Text;

public class VelocitySupportTest : ISerializationTester<Velocity>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Velocity velocity) => velocity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Velocity> Interesting()
        {
            yield return Velocity.Of(21).Si<Metre>().Per.Si<Second>();
            yield return Velocity.Of(342).Si<Kilo, Metre>().Per.Metric<Hour>();
            yield return Velocity.Of(6).Imperial<Mile>().Per.Metric<Hour>();
            yield return Velocity.Of(-41).Imperial<Foot>().Per.Si<Second>();
            yield return Velocity.Of(1.21).Metric<Ångström>().Per.Si<Micro, Second>();
            yield return Velocity.Of(0.125).Metric<AstronomicalUnit>().Per.Metric<Minute>();
            yield return Velocity.Of(0.000292002383).NonStandard<LightYear>().Per.Metric<Week>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
