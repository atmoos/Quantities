namespace Quantities.Serialization.Text.Json.Text;

public class EnergySupportTest : ISerializationTester<Energy>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Energy quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Energy> Interesting()
        {
            yield return Energy.Of(21).Si<Kilo, Joule>();
            // ToDo: Add Wh!
            yield return Energy.Of(342).Metric<Milli, Watt, Minute>();
            yield return Energy.Of(342).Metric<Kilo, Watt, Hour>();
            yield return Energy.Of(6).Si<Mega, Watt, Second>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
