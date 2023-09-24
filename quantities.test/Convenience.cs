using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Numerics;
using Xunit.Sdk;

using static Quantities.Extensions;

namespace Quantities.Test;
public static class Convenience
{
    private const Int32 fullPrecision = 16;
    public static Int32 FullPrecision => fullPrecision;
    public static Int32 MediumPrecision => fullPrecision - 1;
    public static Int32 LowPrecision => fullPrecision - 2;
    public static Int32 VeryLowPrecision => fullPrecision - 3;
    public static Double Uniform(this Random rand) => 2d * (rand.NextDouble() - 0.5);
    public static String Join(String leftUnit, String rightUnit) => $"{leftUnit}\u200C{rightUnit}";
    internal static void IsSameAs(this Quantity actual, Quantity expected, Int32 precision = fullPrecision)
    {
        ReformatEqualMessage((e, a, p) => PrecisionIsBounded(e, a, p), expected, actual, precision);
        Assert.True(actual.HasSameMeasure(in expected), $"Measure mismatch: {actual} != {expected}");
    }
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected, Int32 precision = fullPrecision)
        where TQuantity : struct, IQuantity<TQuantity>, IDimension
    {
        ReformatEqualMessage((e, a, p) => a.Equal(e, p), expected, actual, precision);
        Assert.True(actual.Value.HasSameMeasure(expected.Value), $"Measure mismatch: {actual} != {expected}");
    }
    public static void Equal<T>(this T actual, T expected, Int32 precision = fullPrecision)
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
    public static void IsTrue(this Boolean condition, [CallerArgumentExpression(nameof(condition))] String test = "") => Assert.True(condition, test);
    public static void IsFalse(this Boolean condition, [CallerArgumentExpression(nameof(condition))] String test = "") => Assert.False(condition, test);

    // construct polynomials such that they are not simplified upon construction.
    internal static Polynomial Poly(in Double nominator = 1, in Double denominator = 1, in Double offset = 0)
        => Polynomial.Of(nominator * new Transformation() + offset) * Polynomial.Of(new Transformation() / denominator);
    private static void ReformatEqualMessage<T>(Action<T, T, Int32> assertion, T expected, T actual, Int32 precision)
        where T : IFormattable
    {
        try {
            assertion(expected, actual, precision);
        }
        catch (EqualException) {
            var actualValue = actual.ToString(RoundTripFormat);
            var expectedValue = expected.ToString(RoundTripFormat);
            throw new EqualException(expectedValue, actualValue, precision, precision + 1);
        }
    }
}

internal static class Dim<TSelf>
    where TSelf : IDimension
{
    public static Dim Value => TSelf.D;
    public static Dim Pow(Int32 n) => TSelf.D.Pow(n);
    public static Dim Times<TOther>()
        where TOther : IDimension => TSelf.D * TOther.D;
    public static Dim Per<TOther>()
        where TOther : IDimension => TSelf.D / TOther.D;
}
