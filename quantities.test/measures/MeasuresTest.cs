using Quantities.Measures;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    [Fact]
    public void SiToSi()
    {
        var kilometre = Si<Kilo, Metre>.Conversion.Evaluate(1);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Si<Milli, Metre>.Conversion.Inverse(1);

        Assert.Equal(1000d, millimetre);
    }
}
