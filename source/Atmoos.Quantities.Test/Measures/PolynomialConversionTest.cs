using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Imperial.Temperature;
using Atmoos.Quantities.Units.Si.Derived;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

public class PolynomialConversionTest
{

    [Fact]
    public void FromKmToMiles()
    {
        const Double value = 3;
        const Double expected = 3 / 1.609344;

        Double actual = Evaluate<Si<Kilo, Metre>, Imperial<Mile>>(value);

        PrecisionIsBounded(expected, actual);
    }

    [Fact]
    public void FromSquareKmToSquareMiles()
    {
        const Double value = 3;
        const Double expected = value / (1.609344 * 1.609344);

        Double actual = Evaluate<Power<Si<Kilo, Metre>, Two>, Power<Imperial<Mile>, Two>>(value);

        PrecisionIsBounded(expected, actual, LowPrecision);
    }

    [Fact]
    public void FromCelsiusToMilliKelvin()
    {
        const Double value = -273.15 + 14.65676e-3;
        const Double expected = 14.65676;

        Double actual = Evaluate<Metric<Celsius>, Si<Milli, Kelvin>>(value);

        PrecisionIsBounded(expected, actual, VeryLowPrecision - 2);
    }

    [Fact]
    public void FromCelsiusToFahrenheit()
    {
        const Double value = 100;
        const Double expected = 212;

        Double actual = Evaluate<Metric<Celsius>, Imperial<Fahrenheit>>(value);

        PrecisionIsBounded(expected, actual);
    }

    [Fact]
    public void FromMetrePerSecondToMilePerHour()
    {
        const Double value = 0.44704;
        const Double expected = 1;


        Double actual = Evaluate<Product<Si<Metre>, Power<Si<Second>, Negative<One>>>, Product<Imperial<Mile>, Power<Metric<Hour>, Negative<One>>>>(value);

        PrecisionIsBounded(expected, actual);
    }

    private static Double Evaluate<TSource, TTarget>(Double value)
        where TSource : IMeasure
        where TTarget : IMeasure
    {
        const Int32 approximatelyEqual = 4;
        var source = TSource.Poly;
        var target = TTarget.Poly;

        var expected = target / (source * value);
        var actual = source / target * value;

        Assert.Equal(expected, actual, approximatelyEqual);
        return actual;
    }
}
