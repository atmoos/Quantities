using Quantities.Prefixes;

namespace Quantities.Units.Si;

/// <summary>
/// Metric units are accepted by the SI system, but are themselves not considered to be
//  SI units.
/// </summary>
public interface IMetricUnit : ITransform, IUnit
{
    static Transformation ITransform.ToSi(Transformation self) => self;
}
public interface IMetricUnit<TPrefix> : IMetricUnit
    where TPrefix : IMetricPrefix
{
    static Transformation ITransform.ToSi(Transformation self) => TPrefix.ToSi(self);
}
