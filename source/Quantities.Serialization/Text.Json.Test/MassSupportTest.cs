using Quantities.Units.Imperial.Mass;
using Quantities.Units.NonStandard.Mass;

namespace Quantities.Serialization.Text.Json.Test;

public class MassSupportTest : ISerializationTester<Mass>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Mass area) => area.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Mass> Interesting()
        {
            yield return Mass.Of(21).Si<Kilogram>();
            yield return Mass.Of(342).Metric<Kilo, Gram>();
            yield return Mass.Of(342).Metric<Micro, Gram>();
            yield return Mass.Of(6).Imperial<Pound>();
            yield return Mass.Of(-41).Imperial<Ton>();
            yield return Mass.Of(1.21).Imperial<Ounce>();
            yield return Mass.Of(121).Imperial<Stone>();
            yield return Mass.Of(95.2).Metric<Tonne>();
            yield return Mass.Of(-11).NonStandard<Pfund>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
