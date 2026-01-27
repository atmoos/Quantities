namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class PowerSupportTest : ISerializationTester<Power>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Power quantity) => quantity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Power> Interesting()
        {
            yield return Power.Of(21, Si<Watt>());
            yield return Power.Of(342, Si<Milli, Watt>());
            yield return Power.Of(6, Si<Peta, Watt>());
            yield return Power.Of(-41, Metric<HorsePower>());
            yield return Power.Of(-11, Imperial<Units.Imperial.Power.HorsePower>());
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
