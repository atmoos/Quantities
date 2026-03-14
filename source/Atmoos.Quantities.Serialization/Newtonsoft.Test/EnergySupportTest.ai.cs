namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class EnergySupportTest : ISerializationTester<Energy>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Energy quantity) => quantity.SupportsSerialization();

    public static TheoryData<Energy> Quantities() => [
            Energy.Of(21, Si<Kilo, Joule>()),
            Energy.Of(21, Si<Watt>().Times(Metric<Hour>())),
            Energy.Of(342, Si<Milli, Watt>().Times(Metric<Minute>())),
            Energy.Of(342, Si<Kilo, Watt>().Times(Metric<Hour>())),
            Energy.Of(6, Si<Mega, Watt>().Times(Si<Second>())),
        ];
}
