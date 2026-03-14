namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class PowerSupportTest : ISerializationTester<Power>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Power quantity) => quantity.SupportsSerialization();

    public static TheoryData<Power> Quantities() => [
            Power.Of(21, Si<Watt>()),
            Power.Of(342, Si<Milli, Watt>()),
            Power.Of(6, Si<Peta, Watt>()),
            Power.Of(-41, Metric<HorsePower>()),
            Power.Of(-11, Imperial<Units.Imperial.Power.HorsePower>()),
        ];
}
