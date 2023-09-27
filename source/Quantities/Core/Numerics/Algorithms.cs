using System.Numerics;

namespace Quantities.Core.Numerics;

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
    public static (Double n, Double d) Simplify(Double n, Double d)
    {
        if (Double.IsInteger(n) && Double.IsInteger(d) && Double.Abs(Double.MaxMagnitude(n, d)) < Int64.MaxValue) {
            Int64 gcd = Gcd((Int64)n, (Int64)d);
            return gcd <= 1 ? (n, d) : (n / gcd, d / gcd);
        }
        return (n, d);
    }
}
