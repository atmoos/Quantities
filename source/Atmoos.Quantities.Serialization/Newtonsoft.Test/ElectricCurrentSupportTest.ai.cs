namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class ElectricCurrentSupportTest : ISerializationTester<ElectricCurrent>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricCurrent quantity) => quantity.SupportsSerialization();

    public static TheoryData<ElectricCurrent> Quantities() => [
            ElectricCurrent.Of(21, Si<Ampere>()),
            ElectricCurrent.Of(342, Si<Femto, Ampere>()),
            ElectricCurrent.Of(6, Si<Kilo, Ampere>()),
        ];
}
