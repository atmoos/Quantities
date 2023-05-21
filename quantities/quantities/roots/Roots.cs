using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class SiRoot<TSiUnit> : IRoot
    where TSiUnit : struct, ISiUnit
{
    public static Quant One { get; } = 1d.As<Si<TSiUnit>>();
    public static Quant Zero { get; } = 0d.As<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.As<Si<TSiUnit>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.As<Si<TPrefix, TSiUnit>>();
}

internal sealed class MetricRoot<TMetricUnit> : IRoot
    where TMetricUnit : struct, IMetricUnit
{
    public static Quant One { get; } = 1d.As<Metric<TMetricUnit>>();
    public static Quant Zero { get; } = 0d.As<Metric<TMetricUnit>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.As<Metric<TMetricUnit>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.As<Metric<TPrefix, TMetricUnit>>();
}