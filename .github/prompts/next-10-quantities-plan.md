# Plan: Next 10 Quantities

## Existing Quantities (28)

Acceleration, AmountOfSubstance, Angle, Area, Data, DataRate, Density, ElectricalResistance, ElectricCharge, ElectricCurrent, ElectricPotential, Energy, Force, Frequency, Impulse, Length, LuminousIntensity, Mass, MassFlowRate, Momentum, Power, Pressure, SpecificEnergy, Temperature, Time, Velocity, Volume, VolumetricFlowRate.

---

## Theme 1: Rotational Mechanics (3 quantities)

These extend the existing kinematic/dynamic quantities into the rotational domain. `Angle` already exists as a dimensionless quantity, making these natural next steps.

| # | Quantity | Dimension | SI Unit | Rationale |
|---|---------|-----------|---------|-----------|
| 1 | **Torque** | Force × Length (M·L²·T⁻²) | N·m | Ubiquitous in mechanical engineering. Dimensionally identical to Energy but semantically distinct (rotational vs. translational). |
| 2 | **AngularVelocity** | Angle / Time (T⁻¹) | rad/s | Fundamental rotational kinematic quantity. Bridges Angle and Time. |
| 3 | **AngularAcceleration** | Angle / Time² (T⁻²) | rad/s² | Completes the rotational kinematic trio alongside Angle and AngularVelocity. |

**Dependencies:** Torque needs `IForce` × `ILength`; AngularVelocity and AngularAcceleration need `IAngle` and `ITime`. All dimension interfaces already exist.

**New dimension interfaces needed:**

- `ITorque : IProduct<IForce, ILength>, IMultiplicity<ITorque, One>, IDerivedQuantity`
- `IAngularVelocity : IProduct<IAngle, IDimension<ITime, Negative<One>>>, IMultiplicity<IAngularVelocity, One>, IDerivedQuantity`
- `IAngularAcceleration : IProduct<IAngle, IDimension<ITime, Negative<Two>>>, IMultiplicity<IAngularAcceleration, One>, IDerivedQuantity`

**Suggested implementation order:** AngularVelocity → AngularAcceleration → Torque

---

## Theme 2: Electromagnetic Quantities (4 quantities)

The library currently has ElectricCurrent, ElectricCharge, ElectricPotential, and ElectricalResistance. These four additions complete the core electromagnetic set, covering all remaining SI derived electromagnetic units with special names.

| # | Quantity | Dimension | SI Unit | Rationale |
|---|---------|-----------|---------|-----------|
| 4 | **ElectricalConductance** | 1 / Resistance | S (Siemens) | Inverse of ElectricalResistance. Commonly used in circuit analysis. Completes the resistance/conductance duality. |
| 5 | **Capacitance** | Charge / Potential (I²·T⁴·M⁻¹·L⁻²) | F (Farad) | Fundamental passive circuit element quantity. |
| 6 | **MagneticFlux** | Potential × Time (M·L²·T⁻²·I⁻¹) | Wb (Weber) | Prerequisite for MagneticFluxDensity. Fundamental electromagnetic quantity. |
| 7 | **MagneticFluxDensity** | MagneticFlux / Area (M·T⁻²·I⁻¹) | T (Tesla) | Key quantity for electromagnetic applications (motors, sensors, MRI). |

**Dependencies:** ElectricalConductance is the inverse of ElectricalResistance (already exists). Capacitance needs ElectricCharge and ElectricPotential (both exist). MagneticFlux needs ElectricPotential and Time (both exist). MagneticFluxDensity needs MagneticFlux (new) and Area (exists).

**New dimension interfaces needed:**

- `IElectricalConductance : ILinear<IElectricalConductance>, IDerivedQuantity` (with `IInvertible` linking to `IElectricalResistance`)
- `ICapacitance : ILinear<ICapacitance>, IDerivedQuantity`
- `IMagneticFlux : IProduct<IElectricPotential, ITime>, IMultiplicity<IMagneticFlux, One>, IDerivedQuantity`
- `IMagneticFluxDensity : IProduct<IMagneticFlux, IDimension<ILength, Negative<Two>>>, IMultiplicity<IMagneticFluxDensity, One>, IDerivedQuantity`

**Suggested implementation order:** ElectricalConductance → Capacitance → MagneticFlux → MagneticFluxDensity

---

## Theme 3: Photometry & Fluid Dynamics (3 quantities)

These fill important practical gaps: photometry builds on `LuminousIntensity` (which currently has no derived quantity using it), and dynamic viscosity extends the existing fluid mechanics quantities.

| # | Quantity | Dimension | SI Unit | Rationale |
|---|---------|-----------|---------|-----------|
| 8 | **LuminousFlux** | LuminousIntensity × SolidAngle (≈ cd) | lm (Lumen) | First derived photometric quantity. Makes `LuminousIntensity` practically useful. |
| 9 | **Illuminance** | LuminousFlux / Area (cd·m⁻²) | lx (Lux) | Essential for lighting design and regulation. Builds on LuminousFlux. |
| 10 | **DynamicViscosity** | Pressure × Time (M·L⁻¹·T⁻¹) | Pa·s | Fundamental fluid property. Extends the existing flow rate quantities (MassFlowRate, VolumetricFlowRate). |

**Dependencies:** LuminousFlux needs `ILuminousIntensity` (exists); since solid angle is dimensionless by SI definition (like `IAngle`), LuminousFlux is essentially linear in luminous intensity. Illuminance needs LuminousFlux (new) and Area (exists). DynamicViscosity needs Pressure and Time (both exist).

**New dimension interfaces needed:**

- `ILuminousFlux : ILinear<ILuminousFlux>, IDerivedQuantity`
- `IIlluminance : IProduct<ILuminousFlux, IDimension<ILength, Negative<Two>>>, IMultiplicity<IIlluminance, One>, IDerivedQuantity`
- `IDynamicViscosity : IProduct<IPressure, ITime>, IMultiplicity<IDynamicViscosity, One>, IDerivedQuantity`

**Suggested implementation order:** LuminousFlux → Illuminance → DynamicViscosity

---

## Overall Implementation Order

Taking inter-dependencies into account:

| Phase | Quantities | Notes |
|-------|-----------|-------|
| **Phase 1** | AngularVelocity, AngularAcceleration, ElectricalConductance | No new quantity dependencies; only existing dimension interfaces needed. |
| **Phase 2** | Torque, Capacitance, LuminousFlux, DynamicViscosity | Independent of each other; can be done in parallel. |
| **Phase 3** | MagneticFlux, MagneticFluxDensity, Illuminance | MagneticFluxDensity depends on MagneticFlux; Illuminance depends on LuminousFlux. |

## Per-Quantity Checklist

Each quantity requires:

1. **Dimension interface** — add to `DerivedDimensions.cs` or `ElectricalDimesions.cs`
2. **Quantity struct** — new `.ai.cs` file in `Quantities/`
3. **SI derived unit** — e.g. `NewtonMetre`, `Siemens`, `Farad`, `Weber`, `Tesla`, `Lumen`, `Lux` in `Atmoos.Quantities.Units`
4. **Physics extensions** — operator relationships in `Physics/`
5. **Tests** — unit tests for conversions and arithmetic
6. **Update readme** — add the quantity to the list in `readme.md`
