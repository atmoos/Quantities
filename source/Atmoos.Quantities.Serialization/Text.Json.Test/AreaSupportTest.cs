using Atmoos.Quantities.Units.Imperial.Area;
using Atmoos.Quantities.Units.Imperial.Length;
using Atmoos.Quantities.Units.NonStandard.Area;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class AreaSupportTest : ISerializationTester<Area>, IInjectedUnitTester<Area>
{
    private static Creation.Scalar<Are> Are() => Metric<Are>();

    private static Creation.Scalar<Acre> Acre() => Imperial<Acre>();

    private static Creation.Scalar<Perch> Perch() => Imperial<Perch>();

    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Area quantity) => quantity.SupportsSerialization();

    public static TheoryData<Area> Quantities() => [
            Area.Of(21, Are()),
            Area.Of(342, Acre()),
            Area.Of(6, Perch()),
            Area.Of(-41, Square(Si<Metre>())),
            Area.Of(1.21, Square(Si<Pico, Metre>())),
            Area.Of(121, Square(Si<Kilo, Metre>())),
            Area.Of(95.2, Square(Metric<Ångström>())),
            Area.Of(-11, Square(Imperial<Yard>())),
            Area.Of(9, Square(Imperial<Foot>())),
        ];

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

    public static TheoryData<Area> InjectingAreas() => [
            Area.Of(1, Are()),
            Area.Of(2, Acre()),
            Area.Of(3, Imperial<Rood>()),
            Area.Of(4, Perch()),
            Area.Of(5, NonStandard<Morgen>()),
        ];
}
