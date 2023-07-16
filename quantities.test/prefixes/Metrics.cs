using Quantities.Numerics;
using static Quantities.Extensions;

namespace Quantities.Test.Prefixes;

public static class Metrics<TPrefix>
    where TPrefix : IPrefix
{
    private static readonly Polynomial conversion = PolynomialOf<TPrefix>();
    public static Double MaxValue() => conversion.Evaluate(1);
    public static Double Normalize(in Double value) => conversion.Inverse(in value);
}
