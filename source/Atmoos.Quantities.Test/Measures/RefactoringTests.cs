using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureMultiplicationTest_
{
    [Fact]
    public void LitresIsSameAsCubicDecimetres()
    {
        Expect<Power<Si<Deci, Metre>, Three>>.ToBeEqualTo<Metric<Litre>>();
    }
}
