using Atmoos.Quantities.Core.Numerics;

// ToDo: These should be defined in terms of the base dimensions or other derived dimensions, not as fresh interface definitions.

namespace Atmoos.Quantities.Dimensions;

public interface IElectricPotential : IDimension<IElectricPotential, One>, IDerivedQuantity<IElectricPotential>; // marker interface

public interface IElectricalResistance : IProduct<IElectricPotential, Factor<IElectricCurrent, Negative<One>>>, IMultiplicity<IElectricalConductance, Negative<One>>, ILinear, IDerivedQuantity<IElectricalResistance>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IElectricalConductance : IProduct<IElectricCurrent, Factor<IElectricPotential, Negative<One>>>, IMultiplicity<IElectricalConductance, One>, IMultiplicity<IElectricalResistance, Negative<One>>, ILinear, IDerivedQuantity<IElectricalConductance>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface ICapacitance : IProduct<IElectricCharge, Factor<IElectricPotential, Negative<One>>>, IDerivedQuantity<ICapacitance>; // marker interface

public interface IElectricCharge : IProduct<IElectricCurrent, ITime>, IDerivedQuantity<IElectricCharge>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IMagneticFlux : IProduct<IElectricPotential, ITime>, IDerivedQuantity<IMagneticFlux>; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IMagneticFluxDensity : IProduct<IMagneticFlux, Factor<ILength, Negative<Two>>>, IDerivedQuantity<IMagneticFluxDensity>; // marker interface

public interface IAmountOfInformation : IDimension<IAmountOfInformation, One>, IDerivedQuantity<IAmountOfInformation>; // marker interface

public interface IInformationRate : IProduct<IAmountOfInformation, Factor<ITime, Negative<One>>>, IDerivedQuantity<IInformationRate>; // marker interface
