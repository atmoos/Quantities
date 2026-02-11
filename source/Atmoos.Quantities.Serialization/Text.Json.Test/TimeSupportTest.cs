namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class TimeSupportTest : ISerializationTester<Time>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Time time) => time.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Time> Interesting()
        {
            yield return Time.Of(21, Si<Second>());
            yield return Time.Of(342, Si<Ronto, Second>());
            yield return Time.Of(6, Si<Deci, Second>());
            yield return Time.Of(-41, Metric<Minute>());
            yield return Time.Of(1.21, Metric<Hour>());
            yield return Time.Of(121, Metric<Day>());
            yield return Time.Of(95.2, Metric<Week>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
