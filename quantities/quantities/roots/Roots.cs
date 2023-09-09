using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class SiRoot<TSiUnit> : IRoot
    where TSiUnit : struct, ISiUnit, IDimension
{
    public static Quantity One { get; } = 1d.To<Si<TSiUnit>>();
    public static Quantity Zero { get; } = 0d.To<Si<TSiUnit>>();
    Quantity IPrefixInject<Quantity>.Identity(in Double value) => value.To<Si<TSiUnit>>();
    Quantity IPrefixInject<Quantity>.Inject<TPrefix>(in Double value) => value.To<Si<TPrefix, TSiUnit>>();
}

internal sealed class MetricRoot<TMetricUnit> : IRoot
    where TMetricUnit : struct, IMetricUnit, IDimension
{
    public static Quantity One { get; } = 1d.To<Metric<TMetricUnit>>();
    public static Quantity Zero { get; } = 0d.To<Metric<TMetricUnit>>();
    Quantity IPrefixInject<Quantity>.Identity(in Double value) => value.To<Metric<TMetricUnit>>();
    Quantity IPrefixInject<Quantity>.Inject<TPrefix>(in Double value) => value.To<Metric<TPrefix, TMetricUnit>>();
}
