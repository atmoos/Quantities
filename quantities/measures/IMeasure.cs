using Quantities.Dimensions;
using Quantities.Numerics;

namespace Quantities.Measures;

internal interface IMeasure : IRepresentable, ISerialize
{
    public static abstract Polynomial Poly { get; }
    static abstract Rank Rank<TMeasure>() where TMeasure : IMeasure;
    static abstract Rank RankOf<TDimension>() where TDimension : IDimension;
    public static abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    public static abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
}
