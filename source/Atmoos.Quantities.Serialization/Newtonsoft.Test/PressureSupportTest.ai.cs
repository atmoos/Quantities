using Atmoos.Quantities.Units.NonStandard.Pressure;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class PressureSupportTest : ISerializationTester<Pressure>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Pressure quantity) => quantity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Pressure> Interesting()
        {
            yield return Pressure.Of(21, Si<Pascal>());
            yield return Pressure.Of(342, Si<Hecto, Pascal>());
            yield return Pressure.Of(6, Si<Kilo, Pascal>());
            yield return Pressure.Of(-41, Metric<Bar>());
            yield return Pressure.Of(1.21, NonStandard<StandardAtmosphere>());
            yield return Pressure.Of(760, NonStandard<Torr>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
