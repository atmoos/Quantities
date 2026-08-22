using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

// By SI definition, angle is dimensionless and a derived quantity: https://en.wikipedia.org/wiki/SI_derived_unit
public interface IAngle : IProduct<ILength, Factor<ILength, Negative<One>>>, IDerivedQuantity<IAngle>; // marker interface
public interface ISolidAngle : IProduct<IArea, Factor<ILength, Negative<Two>>>, IDerivedQuantity<ISolidAngle>; // marker interface
