using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IArea : IDimension<ILength, Two>, IDerivedQuantity { /* marker interface */ }
public interface IVolume : IDimension<ILength, Three>, IDerivedQuantity { /* marker interface */ }
public interface IVelocity : IQuotient<ILength, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IAcceleration : IQuotient<ILength, IDimension<ITime, Two>>, IDerivedQuantity { /* marker interface */ }
public interface IForce : IQuotient<IProduct<ILength, IMass>, IDimension<ITime, Two>>, IDerivedQuantity { /* marker interface */ }
public interface IPower : ILinear<IPower>, IDerivedQuantity { /* marker interface */ }
public interface IEnergy : IProduct<IPower, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IAngle : IQuotient<ILength, ILength>, IDerivedQuantity { /* marker interface */ }
public interface IFrequency : IDimension<ITime, Negative<One>>, ILinear, IDerivedQuantity { /* marker interface */ }
