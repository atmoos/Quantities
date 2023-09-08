using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IMeasure : IRepresentable, ISerialize
{
    public static abstract Polynomial Poly { get; }
}
