using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;
using Quantities.Units.NonStandard.Area;

namespace Quantities.Serialization.Text.Json.Text;

public class AreaSupportTest : ISerializationTester<Area>, IInjectedUnitTester<Area>
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

    [Theory]
    [MemberData(nameof(InjectingAreas))]
    public void DeserializationSupportsInjectedUnits(Area quantity)
    {
        // when injected units are not respected during deserialization,
        // division results in very different values when using
        // an area created with the standard API or one that was
        // created using deserialization.
        var someLength = Length.Of(2).Si<Metre>();
        var expectedLength = quantity / someLength;

        var deserializedArea = quantity.SerializeRoundRobin();
        var actualLength = deserializedArea / someLength;

        Assert.Equal(expectedLength, actualLength);
    }

    public static IEnumerable<Object[]> InjectingAreas()
    {
        static IEnumerable<Area> Interesting()
        {
            yield return Area.Of(1).Metric<Are>();
            yield return Area.Of(2).Imperial<Acre>();
            yield return Area.Of(3).Imperial<Rood>();
            yield return Area.Of(4).Imperial<Perch>();
            yield return Area.Of(5).NonStandard<Morgen>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
