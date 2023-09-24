using Quantities.Dimensions;
using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IMeasure : IRepresentable, ISerialize
{
    public static abstract Dimension D { get; }
    public static abstract Polynomial Poly { get; }
    public static abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    public static abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
}

internal interface IMeasure<TBasis> : IMeasure
    where TBasis : IDimension
{
    static Dimension IMeasure.D => TBasis.D;
}
