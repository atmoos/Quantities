using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Test;

public static class Convenience
{
    // construct polynomials such that they are not simplified upon construction.
    internal static Polynomial Poly(in Double nominator = 1, in Double denominator = 1, in Double offset = 0) =>
        Polynomial.Of(nominator * new Transformation() + offset) * Polynomial.Of(new Transformation() / denominator);

    internal static Dimension Copy(this Dimension d) => d.Pow(-1).Pow(-1);

    public static TheoryData<TData> ToTheoryData<TData>(params TData[] data) =>
        data.Aggregate(
            new TheoryData<TData>(),
            (td, item) =>
            {
                td.Add(item);
                return td;
            }
        );
}
