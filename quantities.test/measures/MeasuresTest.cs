using Quantities.Measures;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    [Fact]
    public void SiToSi()
    {
        var kilometre = Si<Kilo, Metre>.Poly.Evaluate(1d);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Si<Milli, Metre>.Poly.Inverse(1d);

        Assert.Equal(1000d, millimetre);
    }
}
