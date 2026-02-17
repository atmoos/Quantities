# Next 10 Quantities

**Already implemented (20):** Length, Time, Mass, ElectricCurrent, Temperature, LuminousIntensity, Area, Volume, Velocity, Acceleration, Force, Power, Energy, Frequency, Pressure, ElectricPotential, ElectricalResistance, Data, DataRate, MassFlow.

**Dimension interfaces that already exist but have no quantity struct:** `IElectricCharge`, `IAngle`, `IAmountOfSubstance`.

---

## Next 10 Quantities

| # | Quantity | Category | SI Formula | Units |
|---|---------|----------|-----------|-------|
| 1 | **Angle** | Quotient (ILength/ILength, dimensionless) | L·L⁻¹ | Radian (SI), Degree, Gradian, Revolution, Arcminute |
| 2 | **ElectricCharge** | Product (ITime × IElectricCurrent) | T·I | Coulomb (SI), Ampere-hour, Milliampere-hour |
| 3 | **AmountOfSubstance** | Scalar (base) | N | Mole (SI), Millimole, Micromole |
| 4 | **Density** | Quotient (IMass / ILength³) | M·L⁻³ | kg/m³, g/cm³, g/L, kg/L, lb/ft³ |
| 5 | **Torque** | Product (IForce × ILength) | M·L²·T⁻² | Newton-metre (SI), Pound-foot, Kilogram-force-metre |
| 6 | **VolumetricFlow** | Quotient (IVolume / ITime) — i.e. L³·T⁻¹ | L³·T⁻¹ | m³/s, L/s, L/min, gal/min (GPM), ft³/min |
| 7 | **Momentum** | Product (IMass × IVelocity) | M·L·T⁻¹ | kg·m/s, N·s, g·cm/s |
| 8 | **Capacitance** | Quotient (IElectricCharge / IElectricPotential) | T⁴·I²·M⁻¹·L⁻² | Farad (SI), Microfarad, Nanofarad, Picofarad |
| 9 | **AngularVelocity** | Quotient (IAngle / ITime) | T⁻¹ | rad/s, rpm (revolution/min), deg/s |
| 10 | **DynamicViscosity** | Product (IPressure × ITime) | M·L⁻¹·T⁻¹ | Pascal-second (SI), Poise, Centipoise, Millipascal-second |

### Notes on ordering rationale

- **Angle** is first because it's a prerequisite for AngularVelocity and Torque (cross-product sense), and is ubiquitous across all engineering domains. Its dimension interface already exists.
- **ElectricCharge** is second because its dimension (`IElectricCharge`) already exists and it unlocks Capacitance. Coulomb is a named SI derived unit.
- **AmountOfSubstance** completes the 7 SI base quantities — its dimension interface already exists.
- **Density** is one of the most commonly used derived quantities in practice (materials science, fluid mechanics, structural engineering).
- **Torque** is a mechanical engineering staple. Note: it has the same SI dimensions as Energy (M·L²·T⁻²) but is a distinct physical quantity — it would need its own dimension interface.
- **VolumetricFlow** is essential for fluid mechanics, HVAC, and process engineering.
- **Momentum** is fundamental in mechanics (Newton's second law in impulse form).
- **Capacitance** extends the electrical domain (requires ElectricCharge first).
- **AngularVelocity** is critical for rotational mechanics (requires Angle first).
- **DynamicViscosity** rounds out the fluid dynamics toolkit alongside Density, Pressure, and VolumetricFlow.
