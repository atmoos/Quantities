using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Creation;

[Ai(Model = "GPT", Version = "5.4")]
public class CreatorsTest
{
    [Fact]
    public void ProductTimesScalarMeasurePreservesTermsAndConversionFactor()
    {
        var product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Times(Si<Second>());
        var si = Si<Metre>().Times(Square(Si<Second>()));

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Metric<Hour>>, Si<Second>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Si<Metre>, Power<Si<Second>, Two>>>(7_200_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void ProductTimesPoweredMeasurePreservesTermsAndConversionFactor()
    {
        var product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Times(Square(Si<Second>()));
        var si = Si<Metre>().Times(Metric<Hour>()).Times(Square(Si<Second>()));

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Metric<Hour>>, Power<Si<Second>, Two>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Product<Si<Metre>, Metric<Hour>>, Power<Si<Second>, Two>>>(2_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void ProductPerScalarMeasurePreservesTermsAndConversionFactor()
    {
        var product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Per(Metric<Minute>());
        var si = Si<Metre>();

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Metric<Hour>>, Power<Metric<Minute>, Negative<One>>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Si<Metre>>(120_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void ProductPerPoweredMeasurePreservesTermsAndConversionFactor()
    {
        var product = Si<Kilo, Metre>().Times(Metric<Hour>());
        var chained = product.Per(Square(Si<Second>()));
        var si = Si<Metre>().Per(Si<Second>());

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Metric<Hour>>, Power<Si<Second>, Negative<Two>>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Si<Metre>, Power<Si<Second>, Negative<One>>>>(7_200_000));
        chained.Transform(converted).Matches(actual);
    }

    /*
    {Product<Product<Si<Kilo, Si.Metre>, Power<Metric<Si.Metric.Minute>, Negative<One>>>, Si<Si.Second>>}
    */


    [Fact]
    public void QuotientTimesScalarMeasurePreservesTermsAndConversionFactor()
    {
        var quotient = Si<Kilo, Metre>().Per(Metric<Minute>());
        var chained = quotient.Times(Si<Second>());
        var si = Si<Metre>().Per(Metric<Hour>()).Times(Si<Second>());

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Power<Metric<Minute>, Negative<One>>>, Si<Second>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>, Si<Second>>>(120_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void QuotientTimesPoweredMeasurePreservesTermsAndConversionFactor()
    {
        var quotient = Si<Kilo, Metre>().Per(Metric<Minute>());
        var chained = quotient.Times(Square(Metric<Minute>()));
        var si = Si<Metre>().Per(Metric<Hour>()).Times(Square(Si<Second>()));

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Power<Metric<Minute>, Negative<One>>>, Power<Metric<Minute>, Two>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>, Power<Si<Second>, Two>>>(432_000_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void QuotientPerScalarMeasurePreservesTermsAndConversionFactor()
    {
        var quotient = Si<Kilo, Metre>().Per(Metric<Minute>());
        var chained = quotient.Per(Si<Second>());
        var si = Si<Metre>().Per(Metric<Hour>()).Per(Si<Second>());

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Power<Metric<Minute>, Negative<One>>>, Power<Si<Second>, Negative<One>>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>, Power<Si<Second>, Negative<One>>>>(120_000));
        chained.Transform(converted).Matches(actual);
    }

    [Fact]
    public void QuotientPerPoweredMeasurePreservesTermsAndConversionFactor()
    {
        var quotient = Si<Kilo, Metre>().Per(Metric<Minute>());
        var chained = quotient.Per(Square(Si<Second>()));
        var si = Si<Metre>().Per(Metric<Hour>()).Per(Square(Metric<Minute>()));

        Quantity actual = chained.Create(2);
        actual.Matches(Quantity.Of<Product<Product<Si<Kilo, Metre>, Power<Metric<Minute>, Negative<One>>>, Power<Si<Second>, Negative<Two>>>>(2));

        Quantity converted = si.Transform(actual);
        converted.Matches(Quantity.Of<Product<Product<Si<Metre>, Power<Metric<Hour>, Negative<One>>>, Power<Metric<Minute>, Negative<Two>>>>(432_000_000));
        chained.Transform(converted).Matches(actual);
    }
}
