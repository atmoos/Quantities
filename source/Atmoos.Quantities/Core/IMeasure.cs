using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Core;

internal interface IMeasure : IRepresentable, ISerialize
{
    public static abstract Dimension D { get; }
    public static abstract Polynomial Poly { get; }
    public static abstract TResult InjectLinear<TResult>(IInject<TResult> inject);
    public static abstract TResult InjectInverse<TResult>(IInject<TResult> inject);
    public static abstract IVisitor Visit(IVisitor visitor, Dimension dimension);
}

internal interface IMeasure<TBasis> : IMeasure
    where TBasis : IDimension
{
    static Dimension IMeasure.D => TBasis.D;
}

internal interface IVisitor : IInject<IVisitor>
{
    Result? Build(Polynomial poly);
}
