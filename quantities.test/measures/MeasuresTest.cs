using Quantities.Measures;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    [Fact]
    public void SiToSi()
    {
        var kilometre = Si<Kilo, Metre>.ToSi(1);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Si<Milli, Metre>.FromSi(1);

        Assert.Equal(1000d, millimetre);
    }
}
