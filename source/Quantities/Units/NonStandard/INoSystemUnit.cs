namespace Quantities.Units.NonStandard;

// There are units of measurement that don't belong to any system.
// This is the marker interface for these types of units.
public interface INoSystemUnit : IUnit, ITransform { /* marker interface */ }
