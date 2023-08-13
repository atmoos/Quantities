using Quantities.Measures;
using Quantities.Numerics;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    [Fact]
    public void SiToSi()
    {
        var kilometre = Polynomial.Of<Si<Kilo, Metre>>().Evaluate(1d);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Polynomial.Of<Si<Milli, Metre>>().Inverse(1d);

        Assert.Equal(1000d, millimetre);
    }
}
