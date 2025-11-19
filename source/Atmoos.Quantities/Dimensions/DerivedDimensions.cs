using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IArea : IDimension<ILength, Two>, IDerivedQuantity { /* marker interface */ }
public interface IVolume : IDimension<ILength, Three>, IDerivedQuantity { /* marker interface */ }
public interface IVelocity : IProduct<ILength, IDimension<ITime, Negative<One>>>, IDerivedQuantity { /* marker interface */ }
public interface IAcceleration : IProduct<ILength, IDimension<ITime, Negative<Two>>>, IDerivedQuantity { /* marker interface */ }
public interface IForce : IProduct<IProduct<ILength, IMass>, IDimension<ITime, Negative<Two>>>, IDerivedQuantity { /* marker interface */ }
public interface IPower : ILinear<IPower>, IDerivedQuantity { /* marker interface */ }
public interface IEnergy : IProduct<IPower, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IAngle : IProduct<ILength, IDimension<ILength, Negative<One>>>, IDerivedQuantity { /* marker interface */ }
public interface IFrequency : IDimension<ITime, Negative<One>>, ILinear, IDerivedQuantity { /* marker interface */ }
public interface IPressure : IProduct<IForce, IDimension<ILength, Negative<Two>>>, IDerivedQuantity { /* marker interface */ }

