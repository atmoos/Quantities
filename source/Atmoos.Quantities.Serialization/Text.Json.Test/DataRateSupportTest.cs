using Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation;
using Bytes = Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class DataRateSupportTest : ISerializationTester<DataRate>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(DataRate quantity) => quantity.SupportsSerialization();

    public static TheoryData<DataRate> Quantities() => [
            DataRate.Of(21, Metric<Bit>().Per(Si<Second>())),
            DataRate.Of(342, Metric<Mega, Bytes>().Per(Si<Second>())),
            DataRate.Of(6, Metric<Nibble>().Per(Si<Milli, Second>())),
            DataRate.Of(-41, Binary<Pebi, Bit>().Per(Metric<Minute>())),
            DataRate.Of(1.21, Binary<Mebi, Bytes>().Per(Si<Micro, Second>())),
        ];
}
