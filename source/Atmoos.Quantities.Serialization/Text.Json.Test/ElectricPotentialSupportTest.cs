namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class ElectricPotentialSupportTest : ISerializationTester<ElectricPotential>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricPotential quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<ElectricPotential> Interesting()
        {
            yield return ElectricPotential.Of(21, Si<Volt>());
            yield return ElectricPotential.Of(342, Si<Micro, Volt>());
            yield return ElectricPotential.Of(0.0747, Si<Giga, Volt>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
