namespace Atmoos.Quantities.Dimensions;

public interface IElectricPotential : ILinear<IElectricPotential> { /* marker interface */ }
public interface IElectricalResistance : ILinear<IElectricalResistance> { /* marker interface */ }
public interface IElectricCharge : IProduct<ITime, IElectricCurrent> { /* marker interface */ }
public interface IAmountOfInformation : ILinear<IAmountOfInformation> { /* marker interface */ }
public interface IInformationRate : IQuotient<IAmountOfInformation, ITime>, IDerivedQuantity { /* marker interface */ }
