using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public static class Systems
{
    public static Scalar<TUnit> Si<TUnit>()
        where TUnit : ISiUnit, IDimension => new(Factory.Of<Si<TUnit>>());
    public static Scalar<TUnit> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, IDimension => new(Factory.Of<Si<TPrefix, TUnit>>());
    public static Scalar<TUnit> Metric<TUnit>()
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TUnit>>());
    public static Scalar<TUnit> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TUnit>>());
    public static Scalar<TUnit> Binary<TPrefix, TUnit>()
        where TPrefix : IBinaryPrefix
        where TUnit : IMetricUnit, IDimension => new(Factory.Of<Metric<TPrefix, TUnit>>());
    public static Scalar<TUnit> Imperial<TUnit>()
        where TUnit : IImperialUnit, IDimension => new(Factory.Of<Imperial<TUnit>>());
    public static Scalar<TUnit> NonStandard<TUnit>()
        where TUnit : INonStandardUnit, IDimension => new(Factory.Of<NonStandard<TUnit>>());
    public static Creation.Power<TUnit, Two> Square<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, ILinear, IDimension => new(scalar.Factory);
    public static Creation.Power<TUnit, Three> Cubic<TUnit>(in Scalar<TUnit> scalar) where TUnit : IUnit, ILinear, IDimension => new(scalar.Factory);
}
