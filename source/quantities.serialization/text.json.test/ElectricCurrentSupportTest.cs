namespace Quantities.Serialization.Text.Json.Text;

public class ElectricCurrentSupportTest : ISerializationTester<ElectricCurrent>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(ElectricCurrent quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<ElectricCurrent> Interesting()
        {
            yield return ElectricCurrent.Of(21).Si<Ampere>();
            yield return ElectricCurrent.Of(342).Si<Femto, Ampere>();
            yield return ElectricCurrent.Of(6).Si<Kilo, Ampere>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
