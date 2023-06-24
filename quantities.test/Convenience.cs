using System.Globalization;
using System.Text.Json;
using Quantities.Measures;
using Quantities.Serialization;
using Xunit.Sdk;

namespace Quantities.Test;
public static class Convenience
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions().EnableQuantities();
    private const Int32 fullPrecision = 16;
    public static Int32 FullPrecision => fullPrecision;
    public static Int32 MediumPrecision => fullPrecision - 1;
    public static Int32 LowPrecision => fullPrecision - 2;
    public static Int32 VeryLowPrecision => fullPrecision - 3;

    internal static void HasSameMeasure(this Quant quant, in Quant other)
    {
        Assert.True(quant.MeasureEquals(other), $"Mismatching measures on '{quant}' and '{other}'");
    }

    public static T SupportsSerialization<T>(this T value)
        where T : IEquatable<T>
    {
        var serialized = Serialize(value);
        var deserialized = Deserialize<T>(serialized);

        Assert.Equal(value, deserialized);
        return deserialized;
    }
    public static JsonSerializerOptions EnableQuantities(this JsonSerializerOptions options)
    {
        options.Converters.Add(new QuantitySerialization());
        return options;
    }

    public static T SerializeRoundRobin<T>(this T value) => Deserialize<T>(Serialize(value));
    public static String Serialize<T>(this T value) => Serialize(value, options);
    public static String Serialize<T>(this T value, JsonSerializerOptions options) => JsonSerializer.Serialize(value, options);
    public static T Deserialize<T>(String value) => Deserialize<T>(value, options);
    public static T Deserialize<T>(String value, JsonSerializerOptions options) => JsonSerializer.Deserialize<T>(value, options) ?? throw new Exception($"Deserialization of {typeof(T).Name} failed");
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        actual.Matches(expected, FullPrecision);
    }
    public static void Matches<TQuantity>(this TQuantity actual, TQuantity expected, Int32 precision)
        where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
    {
        PrecisionIsBounded(expected, actual, precision);
        Assert.Equal(expected.ToString(), actual.ToString());
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