using Quantities.Dimensions;
using Quantities.Units.Imperial.Area;
using Quantities.Units.Imperial.Length;
using Quantities.Units.NonStandard.Area;

namespace Quantities.Serialization.Text.Json.Test;

public class AreaSupportTest : ISerializationTester<Area>, IInjectedUnitTester<Area>
{

    private static readonly Creation.Alias<Are, ILength> are = AliasOf<ILength>.Metric<Are>();
    private static readonly Creation.Alias<Acre, ILength> acre = AliasOf<ILength>.Imperial<Acre>();
    private static readonly Creation.Alias<Perch, ILength> perch = AliasOf<ILength>.Imperial<Perch>();
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Area quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Area> Interesting()
        {
            yield return Area.Of(21, in are);
            yield return Area.Of(342, in acre);
            yield return Area.Of(6, in perch);
            yield return Area.Of(-41, Square(Si<Metre>()));
            yield return Area.Of(1.21, Square(Si<Pico, Metre>()));
            yield return Area.Of(121, Square(Si<Kilo, Metre>()));
            yield return Area.Of(95.2, Square(Metric<Ångström>()));
            yield return Area.Of(-11, Square(Imperial<Yard>()));
            yield return Area.Of(9, Square(Imperial<Foot>()));
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
        var someLength = Length.Of(2, Si<Metre>());
        var expectedLength = quantity / someLength;

        var deserializedArea = quantity.SerializeRoundRobin();
        var actualLength = deserializedArea / someLength;

        Assert.Equal(expectedLength, actualLength);
    }

    public static IEnumerable<Object[]> InjectingAreas()
    {
        static IEnumerable<Area> Interesting()
        {
            yield return Area.Of(1, in are);
            yield return Area.Of(2, in acre);
            yield return Area.Of(3, AliasOf<ILength>.Imperial<Rood>());
            yield return Area.Of(4, in perch);
            yield return Area.Of(5, AliasOf<ILength>.NonStandard<Morgen>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
