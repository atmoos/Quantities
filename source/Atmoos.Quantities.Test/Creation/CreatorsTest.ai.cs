using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Creation;

[Ai(Model = "GPT", Version = "5.4")]
public class CreatorsTest
{
    private const String zeroWidthNonJoiner = "\u200C";

    [Fact]
    public void ProductTimesScalarMeasurePreservesTermsAndConversionFactor()
    {
        Product<Metre, Hour> product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Times(Si<Second>());
        var si = Si<Metre>().Times(Square(Si<Second>()));

        Quantity actual = chained.Create(2);
        (Double value, String unit) = actual;

        Assert.Equal(2d, value);
        Assert.Equal($"km{zeroWidthNonJoiner}h{zeroWidthNonJoiner}s", unit);

        Quantity converted = si.Transform(actual);
        Assert.Equal(7_200_000d, (Double)converted);
        Assert.Equal(2d, (Double)chained.Transform(converted));
    }

    [Fact]
    public void ProductTimesPoweredMeasurePreservesTermsAndConversionFactor()
    {
        Product<Metre, Hour> product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Times(Square(Si<Second>()));
        var si = Si<Metre>().Times(Metric<Hour>()).Times(Square(Si<Second>()));

        Quantity actual = chained.Create(2);
        (Double value, String unit) = actual;

        Assert.Equal(2d, value);
        Assert.Equal($"km{zeroWidthNonJoiner}h{zeroWidthNonJoiner}s²", unit);

        Quantity converted = si.Transform(actual);
        Assert.Equal(2_000d, (Double)converted);
        Assert.Equal(2d, (Double)chained.Transform(converted));
    }

    [Fact]
    public void ProductPerScalarMeasurePreservesTermsAndConversionFactor()
    {
        Product<Metre, Hour> product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Per(Metric<Minute>());
        var si = Si<Metre>();

        Quantity actual = chained.Create(2);
        (Double value, String unit) = actual;

        Assert.Equal(2d, value);
        Assert.Equal($"km{zeroWidthNonJoiner}h/min", unit);

        Quantity converted = si.Transform(actual);
        Assert.Equal(120_000d, (Double)converted);
        Assert.Equal(2d, (Double)chained.Transform(converted));
    }

    [Fact]
    public void ProductPerPoweredMeasurePreservesTermsAndConversionFactor()
    {
        Product<Metre, Hour> product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Per(Square(Si<Second>()));
        var si = Si<Metre>().Per(Si<Second>());

        Quantity actual = chained.Create(2);
        (Double value, String unit) = actual;

        Assert.Equal(2d, value);
        Assert.Equal($"km{zeroWidthNonJoiner}h/s²", unit);

        Quantity converted = si.Transform(actual);
        Assert.Equal(7_200_000d, (Double)converted);
        Assert.Equal(2d, (Double)chained.Transform(converted));
    }
}
