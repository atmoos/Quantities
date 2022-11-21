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
    private static void PrecisionIsBounded(Double expected, Double actual, Int32 precision)
    {
        const Int32 maxPrecision = 15;
        Assert.Equal(expected, actual, precision);
        if (precision < maxPrecision) {
            Assert.Throws<EqualException>(() => Assert.Equal(expected, actual, precision + 1));
        }
    }
    public static void FormattingMatches(Func<Double, IFormattable> formatterFactory, String unit)
    {
        const String format = "f8";
        const Double value = Math.PI;
        IFormattable formattable = formatterFactory(value);
        CultureInfo formatProvider = CultureInfo.CurrentCulture;
        Assert.Equal($"{value.ToString(format, formatProvider)} {unit}", formattable.ToString(format, formatProvider));
    }
}