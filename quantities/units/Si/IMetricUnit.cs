using Quantities.Prefixes;

namespace Quantities.Units.Si;

/// <summary>
/// Metric units are accepted by the SI system, but are themselves not considered to be
//  SI units.
/// </summary>
public interface IMetricUnit : ITransform, IUnit
{
    static Double ITransform.ToSi(in Double self) => self;
    static Double ITransform.FromSi(in Double value) => value;
}
public interface IMetricUnit<TPrefix> : IMetricUnit
    where TPrefix : IMetricPrefix
{
    static Double ITransform.ToSi(in Double self) => TPrefix.ToSi(in self);
    static Double ITransform.FromSi(in Double value) => TPrefix.FromSi(in value);
}
