using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Factories;

public interface IQuadraticFactory<out TQuantity, TPower, TLinear> : IHigherOrderFactory<TQuantity, TPower, TLinear>
    where TPower : ISquare<TLinear>, IDimension
    where TLinear : IDimension
{ }

public interface ICubicFactory<out TQuantity, TPower, TLinear> : IHigherOrderFactory<TQuantity, TPower, TLinear>
    where TPower : ICubic<TLinear>, IDimension
    where TLinear : IDimension
{ }

public interface IHigherOrderFactory<out TQuantity, TPower, TLinear> : IFactory
    where TPower : IDimension
    where TLinear : IDimension
{
    TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    TQuantity Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TPower, IInjectUnit<TLinear>;
    TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TPower, IInjectUnit<TLinear>;
    TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TPower, IInjectUnit<TLinear>;
}
