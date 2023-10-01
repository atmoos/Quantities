namespace Quantities.Serialization.Text.Json.Test;

public class ElectricalResistanceSupportTest : ISerializationTester<ElectricalResistance>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricalResistance quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<ElectricalResistance> Interesting()
        {
            yield return ElectricalResistance.Of(21).Si<Ohm>();
            yield return ElectricalResistance.Of(342).Si<Pico, Ohm>();
            yield return ElectricalResistance.Of(6).Si<Mega, Ohm>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
