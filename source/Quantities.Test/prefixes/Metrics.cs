using Quantities.Core.Numerics;

namespace Quantities.Test.Prefixes;

public static class Metrics<TPrefix>
    where TPrefix : IPrefix
{
    private static readonly Polynomial conversion = Polynomial.Of<TPrefix>();
    public static Double MaxValue() => conversion * 1d;
    public static Double Normalize(in Double value) => conversion / value;
}
