using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class SiRoot<TSiUnit> : IRoot
    where TSiUnit : struct, ISiUnit
{
    public static Quant One { get; } = 1d.To<Si<TSiUnit>>();
    public static Quant Zero { get; } = 0d.To<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.To<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.To<Si<TPrefix, TSiUnit>>();
}

internal sealed class MetricRoot<TMetricUnit> : IRoot
    where TMetricUnit : struct, IMetricUnit
{
    public static Quant One { get; } = 1d.To<Metric<TMetricUnit>>();
    public static Quant Zero { get; } = 0d.To<Metric<TMetricUnit>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.To<Metric<TMetricUnit>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.To<Metric<TPrefix, TMetricUnit>>();
}