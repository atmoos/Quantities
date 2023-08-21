namespace Quantities.Dimensions;

public interface IElectricPotential : IDimension, ILinear<IElectricPotential> { /* marker interface */ }
public interface IElectricalResistance : IDimension, ILinear<IElectricalResistance> { /* marker interface */ }
public interface IAmountOfInformation : IDimension, ILinear<IAmountOfInformation> { /* marker interface */ }
public interface IInformationRate : IQuotient<IAmountOfInformation, ITime>, ILinear<IInformationRate> { /* marker interface */ }
