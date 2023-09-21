using Quantities.Dimensions;
using Quantities.Prefixes;

namespace Quantities.Units.Si;

/// <summary>
/// Metric units are accepted by the SI system, but are themselves not considered to be
//  SI units.
/// </summary>
public interface IMetricUnit : ITransform, IUnit
{ /* marker interface */ }

public interface IMetricUnit<TSelf, TDimension> : IDerived<TDimension>, IMetricUnit, IUnit
    where TSelf : IMetricUnit<TSelf, TDimension>, TDimension
    where TDimension : IDimension
{
    static Transformation ITransform.ToSi(Transformation self) => TSelf.Derived(new From<TDimension>(self));
}
