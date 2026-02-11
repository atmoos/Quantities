namespace Atmoos.Quantities.Units;

/// <summary>
/// Metric units are accepted by the SI system, but are themselves not considered to be
//  SI units.
/// </summary>
// See: https://en.wikipedia.org/wiki/Metric_system
public interface IMetricUnit : ITransform, IUnit; // marker interface
