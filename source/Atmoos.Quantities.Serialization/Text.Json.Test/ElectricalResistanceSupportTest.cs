namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class ElectricalResistanceSupportTest : ISerializationTester<ElectricalResistance>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricalResistance quantity) => quantity.SupportsSerialization();

    public static TheoryData<ElectricalResistance> Quantities() => [
            ElectricalResistance.Of(21, Si<Ohm>()),
            ElectricalResistance.Of(342, Si<Pico, Ohm>()),
            ElectricalResistance.Of(6, Si<Mega, Ohm>()),
        ];
}
