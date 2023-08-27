using System.Globalization;
using System.Numerics;
using Quantities.Measures;
using Xunit.Sdk;

namespace Quantities.Test;
internal static class Convenience
{
    private const Int32 fullPrecision = 16;
    public static Int32 FullPrecision => fullPrecision;
    public static Int32 MediumPrecision => fullPrecision - 1;
    public static Int32 LowPrecision => fullPrecision - 2;
    public static Int32 VeryLowPrecision => fullPrecision - 3;
    public static String Join(String leftUnit, String rightUnit) => $"{leftUnit}\u200C{rightUnit}";
    public static void IsSameAs(this Quant actual, Quant expected, Int32 precision = fullPrecision)
    {
        PrecisionIsBounded(expected.Value, actual.Value, precision);
        Assert.True(actual.EqualMeasure(in expected), $"Measure mismatch: {actual} != {expected}");
    }
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        actual.Matches(expected, FullPrecision);
    }
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected, Int32 precision)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        Equals(actual, expected, precision);
        Assert.True(actual.Value.EqualMeasure(expected.Value), $"Measure mismatch: {actual} != {expected}");
    }
    public static void Equals<T>(this T actual, T expected, Int32 precision)
    where T : IDivisionOperators<T, T, Double>
    {
        var relativeEquality = actual / expected;
        PrecisionIsBounded(1d, relativeEquality, precision);
    }
    public static void PrecisionIsBounded(Double expected, Double actual, Int32 precision = fullPrecision)
    {
        const Int32 maxDoublePrecision = 16;
        const Int32 maxRoundingPrecision = maxDoublePrecision - 1;
        if (precision >= maxDoublePrecision) {
            if (precision == maxDoublePrecision) {
                Assert.Equal(expected, actual);
                return;
            }
            throw new ArgumentOutOfRangeException(nameof(precision), precision, $"Doubles can't be compared to precisions higher than {maxDoublePrecision}.");
        }
        Assert.Equal(expected, actual, precision);
        if (precision <= maxRoundingPrecision) {
            if (precision == maxRoundingPrecision) {
                Assert.Throws<EqualException>(() => Assert.Equal(expected, actual));
                return;
            }
            Assert.Throws<EqualException>(() => Assert.Equal(expected, actual, precision + 1));
        }
    }
    public static void FormattingMatches(Func<Double, IFormattable> formatterFactory, String unit)
    {
        const String format = "f8";
        const Double value = Math.PI;
        IFormattable formattable = formatterFactory(value);
        CultureInfo formatProvider = CultureInfo.CurrentCulture;
        String actual = formattable.ToString(format, formatProvider);
        String expected = $"{value.ToString(format, formatProvider)} {unit}";
        Assert.Equal(expected, actual);
    }
}