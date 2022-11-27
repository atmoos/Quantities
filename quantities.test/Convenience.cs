using System.Globalization;
using Xunit.Sdk;

namespace Quantities.Test;
public static class Convenience
{
    public static Int32 SiPrecision => 15;
    public static Int32 ImperialPrecision => 14;
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        actual.Matches(expected, SiPrecision);
    }
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected, Int32 precision)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        PrecisionIsBounded(expected, actual, precision);
        Assert.Equal(expected.ToString(), actual.ToString());
    }
    public static void PrecisionIsBounded(Double expected, Double actual, Int32 precision)
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
        if (precision < maxRoundingPrecision) {
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