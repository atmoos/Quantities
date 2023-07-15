using Quantities.Measures;
using static Quantities.Extensions;

namespace Quantities.Test.Measures;

public class MeasuresTest
{
    [Fact]
    public void SiToSi()
    {
        var kilometre = Linear<Si<Kilo, Metre>>().Evaluate(1);

        Assert.Equal(1000d, kilometre);
    }
    [Fact]
    public void SiFromSi()
    {
        var millimetre = Linear<Si<Milli, Metre>>().Inverse(1);

        Assert.Equal(1000d, millimetre);
    }
}
