using Atmoos.Quantities.Units.Imperial.Mass;
using Atmoos.Quantities.Units.NonStandard.Mass;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class MassSupportTest : ISerializationTester<Mass>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Mass area) => area.SupportsSerialization();

    public static TheoryData<Mass> Quantities() => [
            Mass.Of(21, Si<Kilogram>()),
            Mass.Of(342, Metric<Kilo, Gram>()),
            Mass.Of(342, Metric<Micro, Gram>()),
            Mass.Of(6, Imperial<Pound>()),
            Mass.Of(-41, Imperial<Ton>()),
            Mass.Of(1.21, Imperial<Ounce>()),
            Mass.Of(121, Imperial<Stone>()),
            Mass.Of(95.2, Metric<Tonne>()),
            Mass.Of(-11, NonStandard<Pfund>()),
        ];
}
