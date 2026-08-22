using Atmoos.Quantities.Units.Si.Derived.ElectricalConductance;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class ElectricalConductanceSupportTest : ISerializationTester<ElectricalConductance>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricalConductance quantity) => quantity.SupportsSerialization();

    public static TheoryData<ElectricalConductance> Quantities() => [
            ElectricalConductance.Of(21, Si<Siemens>()),
            ElectricalConductance.Of(342, Si<Pico, Siemens>()),
            ElectricalConductance.Of(6, Si<Mega, Siemens>()),
        ];
}
