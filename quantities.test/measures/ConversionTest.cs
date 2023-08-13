using Quantities.Measures;
using Quantities.Numerics;
using Quantities.Units.Imperial.Temperature;
using Quantities.Units.Si.Derived;
using Quantities.Units.Si.Metric;

namespace Quantities.Test.Measures;

public class EvaluateTest
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

        Double actual = Evaluate<Power<Square, Si<Kilo, Metre>>, Power<Square, Imperial<Mile>>>(value);

        PrecisionIsBounded(expected, actual, LowPrecision);
    }

    [Fact]
    public void FromCelsiusToMilliKelvin()
    {
        const Double value = -273.15 + 14.65676e-3;
        const Double expected = 14.65676;

        Double actual = Evaluate<Metric<Celsius>, Si<Milli, Kelvin>>(value);

        PrecisionIsBounded(expected, actual, VeryLowPrecision - 3);
    }

    [Fact]
    public void FromCelsiusToGasMark()
    {
        const Double value = 218d + (1d / 3d);
        const Double expected = 7;

        Double actual = Evaluate<Metric<Celsius>, Imperial<GasMark>>(value);

        PrecisionIsBounded(expected, actual, LowPrecision);
    }

    [Fact]
    public void FromMetrePerSecondToMilePerHour()
    {
        const Double value = 0.44704;
        const Double expected = 1;


        Double actual = Evaluate<Quotient<Si<Metre>, Si<Second>>, Quotient<Imperial<Mile>, Metric<Hour>>>(value);

        PrecisionIsBounded(expected, actual);
    }

    private static Double Evaluate<TLeft, TRight>(Double value)
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        Polynomial poly = Conversion<TLeft, TRight>.Polynomial;
        return poly.Evaluate(value);
    }
}
