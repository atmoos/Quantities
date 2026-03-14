using Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation;
using Bytes = Atmoos.Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public class DataSupportTest : ISerializationTester<Data>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Data quantity) => quantity.SupportsSerialization();

    public static TheoryData<Data> Quantities() => [
            Data.Of(21, Metric<Bit>()),
            Data.Of(342, Metric<Mega, Bytes>()),
            Data.Of(6, Metric<Nibble>()),
            Data.Of(-41, Binary<Pebi, Bit>()),
            Data.Of(1.21, Binary<Mebi, Bytes>())
        ];
}
