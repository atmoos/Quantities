using Atmoos.Quantities.Units.NonStandard.Pressure;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class PressureSupportTest : ISerializationTester<Pressure>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Pressure quantity) => quantity.SupportsSerialization();

    public static TheoryData<Pressure> Quantities() => [
            Pressure.Of(21, Si<Pascal>()),
            Pressure.Of(342, Si<Hecto, Pascal>()),
            Pressure.Of(6, Si<Kilo, Pascal>()),
            Pressure.Of(-41, Metric<Bar>()),
            Pressure.Of(1.21, NonStandard<StandardAtmosphere>()),
            Pressure.Of(760, NonStandard<Torr>()),
        ];
}
