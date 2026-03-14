using Atmoos.Quantities.Units.Imperial.Force;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class ForceSupportTest : ISerializationTester<Force>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Force quantity) => quantity.SupportsSerialization();

    public static TheoryData<Force> Quantities() => [
            Force.Of(21, Si<Newton>()),
            Force.Of(342, Si<Kilo, Newton>()),
            Force.Of(6, Si<Milli, Newton>()),
            Force.Of(-41, Imperial<PoundForce>()),
        ];
}
