﻿namespace Quantities.Dimensions;

public interface IElectricPotential : IDimension, ILinear { /* marker interface */ }
public interface IElectricalResistance : IDimension, ILinear { /* marker interface */ }
public interface IAmountOfInformation : IDimension, ILinear { /* marker interface */ }
public interface IInformationRate : IPer<IAmountOfInformation, ITime>, IDimension { /* marker interface */ }
