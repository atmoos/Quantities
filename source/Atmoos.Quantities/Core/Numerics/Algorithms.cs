using System.Numerics;

using static System.Math;

namespace Atmoos.Quantities.Core.Numerics;

internal static class Algorithms
{
    // Implements: https://en.wikipedia.org/wiki/Euclidean_algorithm
    public static T Gcd<T>(T a, T b)
        where T : INumberBase<T>, IModulusOperators<T, T, T>
    {
        var big = T.MaxMagnitude(a, b);
        var small = T.MinMagnitude(a, b);
        return Impl(T.Abs(big), T.Abs(small));
        static T Impl(T max, T min) => T.IsZero(min) ? max : Impl(min, max % min);
    }

    public static (Double nominator, Double denominator) Simplify(in Double nominator, in Double denominator)
    {
        var (n, d) = Implementation(in nominator, in denominator);
        return d >= 0 ? (n, d) : (-n, -d);

        static (Double nominator, Double denominator) Implementation(in Double nominator, in Double denominator)
        {
            if (nominator == denominator) {
                return nominator == 0 ? (0d, 0d) : (1d, 1d);
            }
            if (Abs(denominator) == 1d) {
                return (nominator, denominator);
            }
            (Double scaledNominator, Double scaledDenominator) = Scaling(in nominator, in denominator);
            Double maxMag = MaxMagnitude(scaledNominator, scaledDenominator);
            Double minMag = MinMagnitude(scaledNominator, scaledDenominator);
            if (minMag > 1d && maxMag < Int64.MaxValue) {
                Int64 gcd = Gcd((Int64)scaledNominator, (Int64)scaledDenominator);
                return gcd <= 1 ? (scaledNominator, scaledDenominator) : (scaledNominator / gcd, scaledDenominator / gcd);
            }
            return (scaledNominator, scaledDenominator);
        }

        static (Double nominator, Double denominator) Scaling(in Double nominator, in Double denominator)
        {
            var (nonInteger, allIntegers) = SelectScalingValue(in nominator, in denominator);
            if (nonInteger == 0d || allIntegers) {
                return (nominator, denominator);
            }
            Double truncate;
            Int32 steps = -1;
            var exp = Abs((Int32)Floor(Log10(nonInteger)));
            do {
                truncate = nonInteger * Pow(10d, exp + ++steps);
            } while (truncate - Floor(truncate) > 0d && steps < 15);
            Double scaling = Pow(10d, exp + steps);
            return (scaling * nominator, scaling * denominator);
        }

        static (Double scaling, Boolean allIntegers) SelectScalingValue(in Double nominator, in Double denominator)
            => (Double.IsInteger(nominator), Double.IsInteger(denominator)) switch {
                (false, true) => (Abs(nominator), false),
                (true, false) => (Abs(denominator), false),
                (false, false) => (MinMagnitude(nominator, denominator), false),
                (true, true) => (Double.NaN, true)
            };
    }

    // See: https://en.wikipedia.org/wiki/Exponentiation_by_squaring#Recursive_version
    public static T Pow<T>(in T value, Int32 exponent)
        where T : IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
    {
        return exponent >= 0 ? Power(in value, exponent) : T.MultiplicativeIdentity / Power(in value, -exponent);
        static T Power(in T value, Int32 n) => n switch {
            0 => T.MultiplicativeIdentity,
            1 => value,
            2 => value * value,
            3 => value * value * value,
            _ => (n & 1) switch { // is the rightmost bit zero or one?
                0 => Power(value * value, n >> 1), // right shift to divide exponent by 2
                _ => value * Power(value * value, n >> 1)
            }
        };
    }
}
