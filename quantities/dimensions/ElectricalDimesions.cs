namespace Quantities.Dimensions;

public interface IElectricPotential : IDimension { /* marker interface */ }
public interface IElectricalResistance : IDimension { /* marker interface */ }
public interface IAmountOfInformation : IDimension, ILinear { /* marker interface */ }
public interface IInformationRate : IPer<IAmountOfInformation, ITime> { /* marker interface */ }
