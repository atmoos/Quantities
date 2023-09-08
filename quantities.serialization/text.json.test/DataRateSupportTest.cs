using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Serialization.Text.Json.Text;

public class DataRateSupportTest : ISerializationTester<DataRate>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(DataRate quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<DataRate> Interesting()
        {
            yield return DataRate.Of(21).Metric<Bit>().Per.Si<Second>();
            yield return DataRate.Of(342).Metric<Mega, Bytes>().Per.Si<Second>();
            yield return DataRate.Of(6).Metric<Nibble>().Per.Si<Milli, Second>();
            yield return DataRate.Of(-41).Binary<Pebi, Bit>().Per.Metric<Minute>();
            yield return DataRate.Of(1.21).Binary<Mebi, Bytes>().Per.Si<Micro, Second>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
