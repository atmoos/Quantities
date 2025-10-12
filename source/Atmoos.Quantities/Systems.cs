using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public static class Systems
{
    public static ref readonly Scalar<TUnit> Si<TUnit>()
        where TUnit : ISiUnit, IDimension => ref Cache<TUnit, Si<TUnit>>.Value;
    public static ref readonly Scalar<TUnit> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IDimension => ref Cache<TUnit, Si<TPrefix, TUnit>>.Value;
    public static ref readonly Scalar<TUnit> Metric<TUnit>()
        where TUnit : IMetricUnit, IDimension => ref Cache<TUnit, Metric<TUnit>>.Value;
    public static ref readonly Scalar<TUnit> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IDimension => ref Cache<TUnit, Metric<TPrefix, TUnit>>.Value;
    public static ref readonly Scalar<TUnit> Binary<TPrefix, TUnit>()
        where TPrefix : IBinaryPrefix
        where TUnit : IMetricUnit, IDimension => ref Cache<TUnit, Metric<TPrefix, TUnit>>.Value;
    public static ref readonly Scalar<TUnit> Imperial<TUnit>()
        where TUnit : IImperialUnit, IDimension => ref Cache<TUnit, Imperial<TUnit>>.Value;
    public static ref readonly Scalar<TUnit> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, IDimension => ref Cache<TUnit, NonStandard<TUnit>>.Value;
    public static Creation.Power<TUnit, Two> Square<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, ILinear, IDimension => new(scalar.Factory);
    public static Creation.Power<TUnit, Three> Cubic<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, ILinear, IDimension => new(scalar.Factory);
}

file static class Cache<TUnit, TMeasure>
    where TMeasure : IMeasure
    where TUnit : IUnit, IDimension
{
    private static readonly Scalar<TUnit> scalar = new(in Factory.Of<TMeasure>());
    public static ref readonly Scalar<TUnit> Value => ref scalar;
}
