using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public interface ISquareFactory<out TQuantity, TPower, TLinear> : IHighDimFactory<TQuantity, TPower, ISquare<TLinear>, TLinear>
    where TPower : ISquare<TLinear>
    where TLinear : IDimension, ILinear
{
    internal ICompoundFactory<TQuantity, TLinear> Square { get; }
}

public interface ICubicFactory<out TQuantity, TPower, TLinear> : IHighDimFactory<TQuantity, TPower, ICubic<TLinear>, TLinear>
    where TPower : ICubic<TLinear>
    where TLinear : IDimension, ILinear
{
    internal ICompoundFactory<TQuantity, TLinear> Cubic { get; }
}

public interface IHighDimFactory<out TQuantity, TPower, TDim, TLinear> : IFactory
    where TPower : TDim
    where TDim : IDimension
    where TLinear : IDimension, ILinear
{
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TPower, IInjectUnit<TLinear>;
}
