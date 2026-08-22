using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

// By SI definition, angle is dimensionless and a derived quantity: https://en.wikipedia.org/wiki/SI_derived_unit
public interface IAngle : IProduct<ILength, IDimension<ILength, Negative<One>>>, IMultiplicity<IAngle, One>, IDerivedQuantity; // marker interface
public interface ISolidAngle : IProduct<IArea, IDimension<ILength, Negative<Two>>>, IMultiplicity<ISolidAngle, One>, IDerivedQuantity; // marker interface
