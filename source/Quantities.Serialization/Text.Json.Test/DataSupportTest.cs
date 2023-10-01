using Quantities.Units.Si.Metric.UnitsOfInformation;
using Bytes = Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Quantities.Serialization.Text.Json.Text;

public class DataSupportTest : ISerializationTester<Data>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Data quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Data> Interesting()
        {
            yield return Data.Of(21).Metric<Bit>();
            yield return Data.Of(342).Metric<Mega, Bytes>();
            yield return Data.Of(6).Metric<Nibble>();
            yield return Data.Of(-41).Binary<Pebi, Bit>();
            yield return Data.Of(1.21).Binary<Mebi, Bytes>();
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
