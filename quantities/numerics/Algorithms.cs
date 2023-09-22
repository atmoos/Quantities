using System.Numerics;

namespace Quantities.Numerics;

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
}
