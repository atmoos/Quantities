using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;

namespace Quantities.Serialization.Text.Json.Text;

public class AreaSupportTest : ISerializationTester<Area>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Area quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Area> Interesting()
        {
            yield return Area.Of(21).Metric<Are>();
            yield return Area.Of(342).Imperial<Acre>();
            yield return Area.Of(6).Imperial<Perch>();
            yield return Area.Of(-41).Square.Si<Metre>();
            yield return Area.Of(1.21).Square.Si<Pico, Metre>();
            yield return Area.Of(121).Square.Si<Kilo, Metre>();
            yield return Area.Of(95.2).Square.Metric<Ångström>();
            yield return Area.Of(-11).Square.Imperial<Yard>();
            yield return Area.Of(9).Square.Imperial<Foot>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
