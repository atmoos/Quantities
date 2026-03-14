namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class ElectricPotentialSupportTest : ISerializationTester<ElectricPotential>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricPotential quantity) => quantity.SupportsSerialization();

    public static TheoryData<ElectricPotential> Quantities() => [
            ElectricPotential.Of(21, Si<Volt>()),
            ElectricPotential.Of(342, Si<Micro, Volt>()),
            ElectricPotential.Of(0.0747, Si<Giga, Volt>()),
        ];
}
