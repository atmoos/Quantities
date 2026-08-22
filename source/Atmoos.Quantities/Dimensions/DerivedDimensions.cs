using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IArea : IDimension<ILength, Two>, IDerivedQuantity<IArea>; // marker interface

public interface IVolume : IDimension<ILength, Three>, IDerivedQuantity<IVolume>; // marker interface

public interface IVelocity : IProduct<ILength, Factor<ITime, Negative<One>>>, IDerivedQuantity<IVelocity>; // marker interface

public interface IAcceleration : IProduct<ILength, Factor<ITime, Negative<Two>>>, IDerivedQuantity<IAcceleration>; // marker interface

public interface IForce : IProduct<IMass, Times<ILength, Factor<ITime, Negative<Two>>>>, IDerivedQuantity<IForce>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface ITorque : IProduct<IForce, ILength>, IDerivedQuantity<ITorque>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IAngularVelocity : IProduct<IAngle, Factor<ITime, Negative<One>>>, IDerivedQuantity<IAngularVelocity>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IAngularAcceleration : IProduct<IAngle, Factor<ITime, Negative<Two>>>, IDerivedQuantity<IAngularAcceleration>; // marker interface

public interface IPower : IDimension<IPower, One>, IDerivedQuantity<IPower>; // marker interface

public interface IEnergy : IProduct<IPower, ITime>, IDerivedQuantity<IEnergy>; // marker interface

public interface IFrequency : IDimension<ITime, Negative<One>>, ILinear, IDerivedQuantity<IFrequency>; // marker interface

public interface IPressure : IProduct<IForce, Factor<ILength, Negative<Two>>>, IDerivedQuantity<IPressure>; // marker interface

public interface IDensity : IProduct<IMass, Factor<ILength, Negative<Three>>>, IDerivedQuantity<IDensity>; // marker interface

public interface IVolumetricFlowRate : IProduct<IVolume, Factor<ITime, Negative<One>>>, IDerivedQuantity<IVolumetricFlowRate>; // marker interface

public interface IMassFlowRate : IProduct<IMass, Factor<ITime, Negative<One>>>, IDerivedQuantity<IMassFlowRate>; // marker interface

public interface IMomentum : IProduct<IMass, IVelocity>, IDerivedQuantity<IMomentum>; // marker interface

public interface IImpulse : IProduct<IForce, ITime>, IDerivedQuantity<IImpulse>; // marker interface

public interface ISpecificEnergy : IProduct<IEnergy, Factor<IMass, Negative<One>>>, IDerivedQuantity<ISpecificEnergy>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface ILuminousFlux : IProduct<ILuminousIntensity, ISolidAngle>, IDerivedQuantity<ILuminousFlux>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IIlluminance : IProduct<ILuminousFlux, Factor<ILength, Negative<Two>>>, IDerivedQuantity<IIlluminance>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IDynamicViscosity : IProduct<IPressure, ITime>, IDerivedQuantity<IDynamicViscosity>; // marker interface
