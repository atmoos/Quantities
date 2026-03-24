using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IArea : IDimension<ILength, Two>, IMultiplicity<IArea, One>, IDerivedQuantity; // marker interface

public interface IVolume : IDimension<ILength, Three>, IMultiplicity<IVolume, One>, IDerivedQuantity; // marker interface

public interface IVelocity : IProduct<ILength, IDimension<ITime, Negative<One>>>, IMultiplicity<IVelocity, One>, IDerivedQuantity; // marker interface

public interface IAcceleration : IProduct<ILength, IDimension<ITime, Negative<Two>>>, IMultiplicity<IAcceleration, One>, IDerivedQuantity; // marker interface

public interface IForce : IProduct<IMass, IProduct<ILength, IDimension<ITime, Negative<Two>>>>, IMultiplicity<IForce, One>, IDerivedQuantity; // marker interface

public interface IPower : ILinear<IPower>, IMultiplicity<IPower, One>, IDerivedQuantity; // marker interface

public interface IEnergy : IProduct<IPower, ITime>, IMultiplicity<IEnergy, One>, IDerivedQuantity; // marker interface

public interface IFrequency : IDimension<ITime, Negative<One>>, IMultiplicity<IFrequency, One>, ILinear, IDerivedQuantity; // marker interface

public interface IPressure : IProduct<IForce, IDimension<ILength, Negative<Two>>>, IMultiplicity<IPressure, One>, IDerivedQuantity; // marker interface

public interface IDensity : IProduct<IMass, IDimension<ILength, Negative<Three>>>, IMultiplicity<IDensity, One>, IDerivedQuantity; // marker interface

public interface IVolumetricFlowRate : IProduct<IVolume, IDimension<ITime, Negative<One>>>, IMultiplicity<IVolumetricFlowRate, One>, IDerivedQuantity; // marker interface

public interface IMassFlowRate : IProduct<IMass, IDimension<ITime, Negative<One>>>, IMultiplicity<IMassFlowRate, One>, IDerivedQuantity; // marker interface

public interface IMomentum : IProduct<IMass, IVelocity>, IMultiplicity<IMomentum, One>, IDerivedQuantity; // marker interface

public interface IImpulse : IProduct<IForce, ITime>, IMultiplicity<IImpulse, One>, IDerivedQuantity; // marker interface

public interface ISpecificEnergy : IProduct<IEnergy, IDimension<IMass, Negative<One>>>, IMultiplicity<ISpecificEnergy, One>, IDerivedQuantity; // marker interface
