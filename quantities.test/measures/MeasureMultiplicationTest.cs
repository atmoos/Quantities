using Quantities.Measures;
using Quantities.Units.Si.Metric;

using static Quantities.Numerics.Polynomial;

namespace Quantities.Test.Measures;

public class MeasureMultiplicationTest
{
    [Fact]
    public void IdentityTimesAnyIsAny()
    {
        var conversion = Expect<Si<Micro, Second>>.IsProductOf<Identity, Si<Micro, Second>>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void AnyTimesIdentityIsAny()
    {
        var conversion = Expect<Si<Pico, Second>>.IsProductOf<Si<Pico, Second>, Identity>();
        Assert.Equal(One, conversion);
    }
    [Fact]
    public void ScalarTimesOtherScalarIsProduct()
    {
        var conversion = Expect<Product<Si<Metre>, Si<Second>>>.IsProductOf<Si<Metre>, Si<Second>>();
        Assert.Equal(One, conversion);
    }

    [Fact]
    public void ScalarTimesSimilarScalarIsSquareScalar()
    {
        var conversion = Expect<Power<Square, Si<Metre>>>.IsProductOf<Si<Metre>, Imperial<Yard>>();
        Assert.Equal(Of<Yard>(), conversion);
    }
    [Fact]
    public void ScalarTimesSquareSimilarScalarIsCubicScalar()
    {
        var conversion = Expect<Power<Cubic, Metric<Hour>>>.IsProductOf<Metric<Hour>, Power<Square, Metric<Day>>>();
        Assert.Equal(Square.Pow(Of<Day>()) / Of<Hour>(), conversion);
    }
}
