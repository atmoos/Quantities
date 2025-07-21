using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si.Metric;
using static Atmoos.Quantities.Core.Numerics.Polynomial;

namespace Atmoos.Quantities.Test.Measures;

public class MeasureMultiplicationTest_
{
    [Fact]
    public void LitresIsSameAsCubicDecimetres()
    {
        Expect<Power<Cubic, Si<Deci, Metre>>>.ToBeEqualTo<Metric<Litre>>();
    }
}
