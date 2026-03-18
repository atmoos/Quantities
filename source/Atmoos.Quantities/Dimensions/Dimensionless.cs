using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IDimensionless<TSelf> : IDimension
    where TSelf : IDimensionless<TSelf>
{
    static Dimension IDimension.D { get; } = Unit.Identity;
}

// By SI definition, angle is dimensionless and a derived quantity: https://en.wikipedia.org/wiki/SI_derived_unit
public interface IAngle : IDimensionless<IAngle>, IMultiplicity<IAngle, One>, IDerivedQuantity; // marker interface
