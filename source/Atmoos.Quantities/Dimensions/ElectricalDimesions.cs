using Atmoos.Quantities.Core.Numerics;

// ToDo: These should be defined in terms of the base dimensions or other derived dimensions, not as fresh interface definitions.

namespace Atmoos.Quantities.Dimensions;

public interface IElectricPotential : ILinear<IElectricPotential>; // marker interface

public interface IElectricalResistance : IProduct<IElectricPotential, IDimension<IElectricCurrent, Negative<One>>>, IMultiplicity<IElectricalResistance, One>, IMultiplicity<IElectricalConductance, Negative<One>>, ILinear, IDerivedQuantity; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IElectricalConductance : IProduct<IElectricCurrent, IDimension<IElectricPotential, Negative<One>>>, IMultiplicity<IElectricalConductance, One>, IMultiplicity<IElectricalResistance, Negative<One>>, ILinear, IDerivedQuantity; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface ICapacitance : IProduct<IElectricCharge, IDimension<IElectricPotential, Negative<One>>>, IMultiplicity<ICapacitance, One>, IDerivedQuantity; // marker interface

public interface IElectricCharge : IProduct<IElectricCurrent, ITime>, IMultiplicity<IElectricCharge, One>, IDerivedQuantity; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IMagneticFlux : IProduct<IElectricPotential, ITime>, IMultiplicity<IMagneticFlux, One>, IDerivedQuantity; // marker interface

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public interface IMagneticFluxDensity : IProduct<IMagneticFlux, IDimension<ILength, Negative<Two>>>, IMultiplicity<IMagneticFluxDensity, One>, IDerivedQuantity; // marker interface

public interface IAmountOfInformation : ILinear<IAmountOfInformation>, IDerivedQuantity; // marker interface

public interface IInformationRate : IProduct<IAmountOfInformation, IDimension<ITime, Negative<One>>>, IMultiplicity<IInformationRate, One>, IDerivedQuantity; // marker interface
