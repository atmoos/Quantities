# Plan: Units for Next 10 Quantities

Accompanies the [next 10 quantities plan](next-10-quantities-plan.md).

## Overview

Each new quantity needs at least one SI derived unit. Some quantities also benefit from metric, imperial, or non-standard units that are in common practical use.

Quantities marked with **(compound only)** have no universally recognised named unit; they are expressed as combinations of existing units (e.g. `N·m`, `Pa·s`). They still need to work with the existing `Scalar<TUnit>`, `Product<T1, T2>`, and `Quotient<TN, TD>` creation patterns but do not require a new unit struct.

---

## Theme 1: Rotational Mechanics

### 1. Torque — compound only

Torque is dimensionally identical to energy (M·L²·T⁻²) but semantically distinct. The SI unit is the **newton metre** (N·m), which is *not* given a special name to preserve this distinction.

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| — | N·m | (compound) | — | Expressed as `Product<Newton, Metre>`. No dedicated unit struct needed. |
| — | lbf·ft | (compound) | 1 lbf·ft = 1.3558179483314 N·m | Expressed as `Product<PoundForce, Foot>`. Both units exist. |
| — | lbf·in | (compound) | 1 lbf·in ≈ 0.1129848290276 N·m | Expressed as `Product<PoundForce, Inch>`. Both units exist. |

**Implementation notes:**
- The quantity struct uses `Product<TForce, TLength>` and `Quotient<TForce, TLength>` for `Of()` and `To()`.
- All torque units are compound: `Product<Newton, Metre>`, `Product<PoundForce, Foot>`, and `Product<PoundForce, Inch>`. No dedicated unit structs needed.

### 2. AngularVelocity — compound only

All angular velocity units are expressible as quotients of existing angle and time units. No dedicated unit structs are needed.

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| — | rad/s | (compound) | identity | Expressed as `Quotient<Radian, Second>`. Both units exist. |
| — | rev/min | (compound) | 1 rev/min = 2π/60 rad/s | Expressed as `Quotient<Turn, Minute>`. Both units exist (`Turn` has representation `"rev"`). |
| — | °/s | (compound) | 1 °/s = π/180 rad/s | Expressed as `Quotient<Degree, Second>`. Both units exist. |

**Implementation notes:**
- The quantity struct uses `Quotient<TAngle, TTime>` for `Of()` and `To()`.
- All angular velocity units are compound. The SI coherent form is `Quotient<Radian, Second>` (rad/s).
- `Turn` already exists (in `NonStandard/Angle/Turn.ai.cs`) with `Representation => "rev"`, so `Quotient<Turn, Minute>` naturally produces "rev/min".

### 3. AngularAcceleration — compound only

Angular acceleration follows the same pattern as (linear) acceleration: it is expressed as a compound of existing units.

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| — | rad/s² | (compound) | identity | Compound of `Radian` and `Second`. Both units exist. |

**Implementation notes:**
- The quantity struct uses a compound creation pattern analogous to linear acceleration.
- No dedicated unit struct is needed; the SI coherent form is composed from `Radian` and `Second`.
- Additional non-standard compound forms can be added later as needed.

---

## Theme 2: Electromagnetic Quantities

### 4. ElectricalConductance — SI derived unit: siemens

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Siemens** | S | SI Derived | identity | Named SI unit. Reciprocal of ohm. |

**Implementation notes:**
- `Siemens` implements `ISiUnit, IElectricalConductance, IInvertible<IElectricalResistance>`.
- The `IInvertible` pattern mirrors how `Hertz` relates to `ITime`: `Siemens` is `1 / Ohm`.
- Implements `ISystemInject<IElectricalResistance>` to inject `Ohm` as the base.

### 5. Capacitance — SI derived unit: farad

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Farad** | F | SI Derived | identity | Named SI unit for capacitance. |

**Implementation notes:**
- `Farad` implements `ISiUnit, ICapacitance`.
- Practical capacitance values are typically very small (pF, nF, µF), so metric prefix support (`Si<Pico, Farad>()`, `Si<Nano, Farad>()`, `Si<Micro, Farad>()`) is essential — this is handled automatically by the prefix system.

### 6. MagneticFlux — SI derived unit: weber

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Weber** | Wb | SI Derived | identity | Named SI unit for magnetic flux. |
| **Maxwell** | Mx | NonStandard | 1 Mx = 10⁻⁸ Wb | CGS unit, still occasionally encountered. |

**Implementation notes:**
- `Weber` implements `ISiUnit, IMagneticFlux`.
- `Maxwell` implements `INonStandardUnit, IMagneticFlux` with `ToSi`: `self / 1e8` (rooted in `Weber`).

### 7. MagneticFluxDensity — SI derived unit: tesla

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Tesla** | T | SI Derived | identity | Named SI unit for magnetic flux density. |
| **Gauss** | G | NonStandard | 1 G = 10⁻⁴ T | CGS unit, very widely used in practice. |

**Implementation notes:**
- `Tesla` implements `ISiUnit, IMagneticFluxDensity`.
- `Gauss` implements `INonStandardUnit, IMagneticFluxDensity` with `ToSi`: `self / 1e4` (rooted in `Tesla`).

---

## Theme 3: Photometry & Fluid Dynamics

### 8. LuminousFlux — SI derived unit: lumen

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Lumen** | lm | SI Derived | identity | Named SI unit. cd·sr, but sr is dimensionless. |

**Implementation notes:**
- `Lumen` implements `ISiUnit, ILuminousFlux`.
- Since steradian (solid angle) is dimensionless, `LuminousFlux` is effectively linear in luminous intensity with its own named unit.

### 9. Illuminance — SI derived unit: lux

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| **Lux** | lx | SI Derived | identity | Named SI unit. lm/m². |
| **FootCandle** | fc | NonStandard | 1 fc ≈ 10.7639 lx | Common in North American lighting practice. |

**Implementation notes:**
- `Lux` implements `ISiUnit, IIlluminance`.
- `FootCandle` implements `INonStandardUnit, IIlluminance` with `ToSi`: `10.7639104167097 * self.RootedIn<Lux>()` (1 lumen per square foot).

### 10. DynamicViscosity — compound only (pascal second)

| Unit | Symbol | Category | Conversion | Notes |
|------|--------|----------|------------|-------|
| — | Pa·s | (compound) | — | Expressed as `Product<Pascal, Second>`. No dedicated named SI unit. |
| **Poise** | P | NonStandard | 1 P = 0.1 Pa·s | CGS unit; often used as centipoise (cP = mPa·s). |

**Implementation notes:**
- Like Torque, DynamicViscosity has no special-name SI unit; it uses `Pa·s`.
- `Poise` implements `INonStandardUnit, IDynamicViscosity` with `ToSi`: `self / 10`.
- In practice, centipoise (cP) is extremely common. With `Poise` defined, `Si<Centi, Poise>()` would not work directly (Poise is non-standard, not metric). Consider also defining a metric `Poiseuille` (= 1 Pa·s) if prefix support is desired, though this name is rarely used.

---

## Summary Table

| Quantity | Named SI Unit | Additional Units | Notes |
|----------|--------------|------------------|-------|
| Torque | — (N·m) | — (lbf·ft, lbf·in) | Compound only; all units are compounds of existing units |
| AngularVelocity | — (rad/s) | — (rev/min, °/s) | Compound only; all units are quotients of existing angle and time units |
| AngularAcceleration | — (rad/s²) | — | Compound only; analogous to linear acceleration |
| ElectricalConductance | Siemens | — | Invertible with Ohm |
| Capacitance | Farad | — | Prefix support critical |
| MagneticFlux | Weber | Maxwell | |
| MagneticFluxDensity | Tesla | Gauss | |
| LuminousFlux | Lumen | — | |
| Illuminance | Lux | FootCandle | |
| DynamicViscosity | — (Pa·s) | Poise | Compound only |

**Total new unit structs: 10** (6 SI derived + 4 non-standard)

Five units originally planned as discrete structs are now compound-only, as all their components already exist:
- RadianPerSecond → `Quotient<Radian, Second>`
- RadianPerSecondSquared → compound of `Radian` / `Second`²
- RevolutionsPerMinute → `Quotient<Turn, Minute>` (`Turn` exists with representation `"rev"`)
- DegreesPerSecond → `Quotient<Degree, Second>`
- PoundForceFoot → `Product<PoundForce, Foot>`

## File Locations

| Unit | Path |
|------|------|
| Siemens | `Units/Si/Derived/ElectricalConductance/Siemens.ai.cs` |
| Farad | `Units/Si/Derived/Capacitance/Farad.ai.cs` |
| Weber | `Units/Si/Derived/MagneticFlux/Weber.ai.cs` |
| Tesla | `Units/Si/Derived/MagneticFluxDensity/Tesla.ai.cs` |
| Lumen | `Units/Si/Derived/LuminousFlux/Lumen.ai.cs` |
| Lux | `Units/Si/Derived/Illuminance/Lux.ai.cs` |
| Maxwell | `Units/NonStandard/MagneticFlux/Maxwell.ai.cs` |
| Gauss | `Units/NonStandard/MagneticFluxDensity/Gauss.ai.cs` |
| FootCandle | `Units/NonStandard/Illuminance/FootCandle.ai.cs` |
| Poise | `Units/NonStandard/DynamicViscosity/Poise.ai.cs` |
