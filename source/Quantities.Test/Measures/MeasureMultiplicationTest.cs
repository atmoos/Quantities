using Quantities.Measures;
using Quantities.Units.Si.Metric;

using static Quantities.Core.Numerics.Polynomial;

namespace Quantities.Test.Measures;

public class MeasureMultiplicationTest
{
    [Fact]
    public void IdentityTimesAnyIsAny()
    {
        var conversion = Expect<Si<Micro, Second>>.ToBeProductOf<Identity, Si<Micro, Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void AnyTimesIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.ToBeProductOf<Si<Pico, Second>, Identity>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarTimesOtherScalarIsProduct()
    {
        var conversion = Expect<Product<Si<Metre>, Si<Second>>>.ToBeProductOf<Si<Metre>, Si<Second>>();
        Assert.Equal(One, conversion);
    }

    [Fact]
    public void ScalarTimesSimilarScalarIsSquareScalar()
    {
        var conversion = Expect<Power<Square, Si<Metre>>>.ToBeProductOf<Si<Metre>, Imperial<Yard>>();
        Assert.Equal(Of<Yard>(), conversion);
    }
    [Fact]
    public void ScalarTimesSquareSimilarScalarIsCubicScalar()
    {
        var conversion = Expect<Power<Cubic, Metric<Hour>>>.ToBeProductOf<Metric<Hour>, Power<Square, Metric<Day>>>();
        Assert.Equal((Of<Day>().Pow(2) / Of<Hour>()).Simplify(), conversion);
    }
}
