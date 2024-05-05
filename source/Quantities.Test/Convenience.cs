using Quantities.Core.Numerics;

namespace Quantities.Test;
public static class Convenience
{
    // construct polynomials such that they are not simplified upon construction.
    internal static Polynomial Poly(in Double nominator = 1, in Double denominator = 1, in Double offset = 0)
        => Polynomial.Of(nominator * new Transformation() + offset) * Polynomial.Of(new Transformation() / denominator);
}
