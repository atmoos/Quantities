using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IElectricPotential : ILinear<IElectricPotential>; // marker interface

public interface IElectricalResistance : ILinear<IElectricalResistance>; // marker interface

public interface IElectricCharge : IProduct<IElectricCurrent, ITime>, IMultiplicity<IElectricCharge, One>, IDerivedQuantity; // marker interface

public interface IAmountOfInformation : ILinear<IAmountOfInformation>, IDerivedQuantity; // marker interface

public interface IInformationRate : IProduct<IAmountOfInformation, IDimension<ITime, Negative<One>>>, IMultiplicity<IInformationRate, One>, IDerivedQuantity; // marker interface
