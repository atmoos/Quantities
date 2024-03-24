using Quantities.Measures;
using Quantities.Units.NonStandard.Velocity;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test.Measures;

public class InversionTest
{
    private const String zeroWidthNonJoiner = "\u200C";
    [Fact]
    public void SiInvertedIsInverseOfSi()
    {
        Expect<Inverse<Si<Kilo, Metre>>>.ToBeInverseOf<Si<Kilo, Metre>>("km⁻¹");
    }

    [Fact]
    public void MetricInvertedIsInverseOfMetric()
    {
        Expect<Inverse<Metric<Milli, Celsius>>>.ToBeInverseOf<Metric<Milli, Celsius>>("m°C⁻¹");
    }

    [Fact]
    public void ImperialInvertedIsInverseOfImperial()
    {
        Expect<Inverse<Imperial<Foot>>>.ToBeInverseOf<Imperial<Foot>>("ft⁻¹");
    }

    [Fact]
    public void NonStandardInvertedIsInverseOfNonStandard()
    {
        Expect<Inverse<NonStandard<Knot>>>.ToBeInverseOf<NonStandard<Knot>>("kn⁻¹");
    }

    [Fact]
    public void ProductInvertedIsInverseOfNonStandard()
    {
        Expect<Inverse<Product<Si<Metre>, Metric<Hour>>>>.ToBeInverseOf<Product<Si<Metre>, Metric<Hour>>>($"m{zeroWidthNonJoiner}h⁻¹");
    }
    [Fact]
    public void QuotientInvertedIsInvertedQuotient()
    {
        Expect<Quotient<Metric<Hour>, Si<Metre>>>.ToBeInverseOf<Quotient<Si<Metre>, Metric<Hour>>>("h/m");
    }
}
