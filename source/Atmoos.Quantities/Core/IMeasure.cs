using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Core;

internal interface IMeasure : IRepresentable, ISerialize
{
    public static abstract ref readonly Kind Kind { get; }
    public static abstract Dimension D { get; }
    public static abstract Polynomial Poly { get; }
    public static abstract IVisitor InjectLinear(IVisitor inject);
    public static abstract TResult InjectInverse<TResult>(IInject<TResult> inject);
    public static abstract IVisitor Power(IVisitor inject, Int32 exponent);
}

internal interface IMeasure<TBasis> : IMeasure
    where TBasis : IDimension
{
    static ref readonly Kind IMeasure.Kind => ref TBasis.Kind;
    static Dimension IMeasure.D { get; } = TBasis.D;
}

internal interface IVisitor : IInject<IVisitor>
{
    Result? Build(Polynomial poly, Dimension target);

    // Only the Arithmetic<TSelf>.Visitor cares about numerator/denominator polarity; everyone else is unaffected.
    IVisitor AsDenominator() => this;
}
