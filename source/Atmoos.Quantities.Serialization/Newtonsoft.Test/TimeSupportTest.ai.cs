namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class TimeSupportTest : ISerializationTester<Time>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Time time) => time.SupportsSerialization();

    public static TheoryData<Time> Quantities() => [
            Time.Of(21, Si<Second>()),
            Time.Of(342, Si<Ronto, Second>()),
            Time.Of(6, Si<Deci, Second>()),
            Time.Of(-41, Metric<Minute>()),
            Time.Of(1.21, Metric<Hour>()),
            Time.Of(121, Metric<Day>()),
            Time.Of(95.2, Metric<Week>()),
        ];
}
