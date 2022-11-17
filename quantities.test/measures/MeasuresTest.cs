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
    [Fact]
    public void MapValue()
    {
        var centiProjection = new Projection<Si<Centi, Metre>>();

        var decaMetre = centiProjection.MapTo<Si<Deca, Metre>>(2000);

        Assert.Equal(2, decaMetre);
    }
}
