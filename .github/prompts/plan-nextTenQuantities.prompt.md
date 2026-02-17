# Next 10 Quantities

**Already implemented (20):** Length, Time, Mass, ElectricCurrent, Temperature, LuminousIntensity, Area, Volume, Velocity, Acceleration, Force, Power, Energy, Frequency, Pressure, ElectricPotential, ElectricalResistance, Data, DataRate, MassFlow.

**Dimension interfaces that already exist but have no quantity struct:** `IElectricCharge`, `IAngle`, `IAmountOfSubstance`.

---

## Next 10 Quantities

| # | Quantity | Category | SI Formula | New Units |
|---|---------|----------|-----------|-------|
| 1 | **Angle** | Quotient (ILength/ILength, dimensionless) | L·L⁻¹ | Radian (SI), Degree, Gradian, Revolution, Arcminute |
| 2 | **ElectricCharge** | Product (ITime × IElectricCurrent) | T·I | Coulomb (SI) |
| 3 | **AmountOfSubstance** | Scalar (base) | N | Mole (SI) |
| 4 | **Density** | Quotient (IMass / ILength³) | M·L⁻³ | _(implied)_ |
| 5 | **Torque** | Product (IForce × ILength) | M·L²·T⁻² | _(implied)_ |
| 6 | **VolumetricFlow** | Quotient (IVolume / ITime) — i.e. L³·T⁻¹ | L³·T⁻¹ | _(implied)_ |
| 7 | **Momentum** | Product (IMass × IVelocity) | M·L·T⁻¹ | _(implied)_ |
| 8 | **Capacitance** | Quotient (IElectricCharge / IElectricPotential) | T⁴·I²·M⁻¹·L⁻² | Farad (SI) |
| 9 | **AngularVelocity** | Quotient (IAngle / ITime) | T⁻¹ | _(implied)_ |
| 10 | **DynamicViscosity** | Product (IPressure × ITime) | M·L⁻¹·T⁻¹ | Poise |

### Notes

**Units**: Most compound quantities don't need dedicated unit structs — compound units (e.g. kg/m³, m³/s, kg·m/s) are implied by the compound `Of<T,V>()` creation methods. Prefix variants (e.g. Millimole, Microfarad) are implied by the SI prefix system. Only truly independent named units need explicit structs.

### Ordering rationale

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
