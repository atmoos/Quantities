using Atmoos.Quantities.Units.Imperial.Area;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class AreaSupportTest : ISerializationTester<Area>
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
}
