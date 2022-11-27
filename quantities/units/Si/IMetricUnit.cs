using Quantities.Prefixes;

namespace Quantities.Units.Si;

/// <summary>
/// Metric units are accepted by the SI system, but are themselves not considered to be
//  SI units.
/// </summary>
public interface IMetricUnit : ITransform, IUnit
{
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
}
public interface IMetricUnit<TPrefix> : IMetricUnit
    where TPrefix : ISiPrefix
{
    static Double ITransform.ToSi(in Double value) => TPrefix.ToSi(in value);
    static Double ITransform.FromSi(in Double value) => TPrefix.FromSi(in value);
}
