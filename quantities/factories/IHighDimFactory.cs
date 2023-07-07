using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public interface ISquareFactory<out TQuantity, TPower, TLinear> : IHighDimFactory<TQuantity, TPower, TLinear>
    where TPower : ISquare<TLinear>, IDimension
    where TLinear : IDimension, ILinear
{ }

public interface ICubicFactory<out TQuantity, TPower, TLinear> : IHighDimFactory<TQuantity, TPower, TLinear>
    where TPower : ICubic<TLinear>, IDimension
    where TLinear : IDimension, ILinear
{ }

public interface IHighDimFactory<out TQuantity, TPower, TLinear> : IFactory
    where TPower : IDimension
    where TLinear : IDimension, ILinear
{
    public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TPower, IInjectUnit<TLinear>;
    public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TPower, IInjectUnit<TLinear>;
}
