using System.Numerics;

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

    public static (Double nominator, Double denominator) Simplify(Double nominator, Double denominator)
    {
        if (Double.IsInteger(denominator) && Double.Abs(Double.MaxMagnitude(nominator, denominator)) < Int64.MaxValue) {
            Int64 gcd = Gcd((Int64)nominator, (Int64)denominator);
            return gcd <= 1 ? (nominator, denominator) : (nominator / gcd, denominator / gcd);
        }
        return (nominator, denominator);
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
