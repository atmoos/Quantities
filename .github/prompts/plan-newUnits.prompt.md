# Plan: New Units for Existing Quantities

Add the following units to existing quantities across `Atmoos.Quantities` and `Atmoos.Quantities.Units`.
Units are grouped by quantity, ordered from most practically valuable to most specialised.

---

## Length

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `Fathom` | Imperial | `Units.Imperial.Length` | `"ftm"` | `6 * DerivedFrom<Foot>()` | [Fathom](https://en.wikipedia.org/wiki/Fathom) |
| `League` | Imperial | `Units.Imperial.Length` | `"lea"` | `3 * DerivedFrom<Mile>()` | [League (unit)](https://en.wikipedia.org/wiki/League_(unit)) |
| `Thou` | Imperial | `Units.Imperial.Length` | `"thou"` | `DerivedFrom<Inch>() / 1000` | [Thousandth of an inch](https://en.wikipedia.org/wiki/Thousandth_of_an_inch) |
| `Hand` | Imperial | `Units.Imperial.Length` | `"hh"` | `4 * DerivedFrom<Inch>()` (= 4 in) | [Hand (unit)](https://en.wikipedia.org/wiki/Hand_(unit)) |
| `Parsec` | NonStandard | `Units.NonStandard.Length` | `"pc"` | `≈ 3.085677581491367e16 * RootedIn<Metre>()` | [Parsec](https://en.wikipedia.org/wiki/Parsec) |
| `Point` | NonStandard | `Units.NonStandard.Length` | `"pt"` | `DerivedFrom<Inch>() / 72` (desktop publishing) | [Point (typography)](https://en.wikipedia.org/wiki/Point_(typography)) |

---

## Mass

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `TroyOunce` | Imperial | `Units.Imperial.Mass` | `"oz t"` | `≈ 31.1034768 * DerivedFrom<Gram>()` | [Troy weight](https://en.wikipedia.org/wiki/Troy_weight) |
| `TroyPound` | Imperial | `Units.Imperial.Mass` | `"lb t"` | `12 * DerivedFrom<TroyOunce>()` (= 373.2417216 g) | [Troy weight](https://en.wikipedia.org/wiki/Troy_weight) |
| `Carat` | NonStandard | `Units.NonStandard.Mass` | `"ct"` | `0.2 * DerivedFrom<Gram>()` | [Carat (mass)](https://en.wikipedia.org/wiki/Carat_(mass)) |
| `Dalton` | NonStandard | `Units.NonStandard.Mass` | `"Da"` | `≈ 1.66053906660e-27 * RootedIn<Kilogram>()` | [Dalton (unit)](https://en.wikipedia.org/wiki/Dalton_(unit)) |

---

## Time

Target project: `Atmoos.Quantities.Units` (under `Si.Metric.Time`)

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `Year` | Metric | `Units.Si.Metric.Time` | `"yr"` | `365.25 * DerivedFrom<Day>()` (Julian year) | [Julian year (astronomy)](https://en.wikipedia.org/wiki/Julian_year_(astronomy)) |
| `Fortnight` | Imperial | `Units.Imperial.Time` | `"fn"` | `14 * DerivedFrom<Day>()` | [Fortnight](https://en.wikipedia.org/wiki/Fortnight) |

Note: `Month` is intentionally excluded — its ambiguous length makes it unsuitable for exact conversion.

---

## Volume

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `Barrel` | Imperial | `Units.Imperial.Volume` | `"bbl"` | `36 * DerivedFrom<Gallon>()` (imperial barrel = 36 imperial gallons ≈ 163.66 L) | [Barrel (unit)](https://en.wikipedia.org/wiki/Barrel_(unit)) |
| `Bushel` | Imperial | `Units.Imperial.Volume` | `"bu"` | `8 * DerivedFrom<Gallon>()` (imperial bushel = 8 imperial gallons) | [Bushel](https://en.wikipedia.org/wiki/Bushel) |

Note: `Cup`, `Tablespoon`, and `Teaspoon` are excluded — they have no standardized definition in British Imperial and are primarily US customary or informally metric-defined cooking measures.

---

## Pressure

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `MmHg` | NonStandard | `Units.NonStandard.Pressure` | `"mmHg"` | `133.322387415 * RootedIn<Pascal>()` | [Millimetre of mercury](https://en.wikipedia.org/wiki/Millimetre_of_mercury) |
| `InHg` | Imperial | `Units.Imperial.Pressure` | `"inHg"` | `25.4 * DerivedFrom<MmHg>()` (1 inch = 25.4 mm exactly; implement after `MmHg`) | [Inch of mercury](https://en.wikipedia.org/wiki/Inch_of_mercury) |
| `TechnicalAtmosphere` | NonStandard | `Units.NonStandard.Pressure` | `"at"` | `98066.5 * RootedIn<Pascal>()` (= kgf/cm²) | [Technical atmosphere](https://en.wikipedia.org/wiki/Technical_atmosphere) |

Note: PSI is implicitly available as `PoundForce / Inch²` but could be added explicitly as a convenience unit named `PoundPerSquareInch` if parsing/display is desired.

---

## Energy

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `Calorie` | NonStandard | `Units.NonStandard.Energy` | `"cal"` | `4.184 * RootedIn<Joule>()` (thermochemical calorie) | [Calorie](https://en.wikipedia.org/wiki/Calorie) |
| `BritishThermalUnit` | Imperial | `Units.Imperial.Energy` | `"BTU"` | `1055.05585262 * RootedIn<Joule>()` | [British thermal unit](https://en.wikipedia.org/wiki/British_thermal_unit) |
| `Electronvolt` | NonStandard | `Units.NonStandard.Energy` | `"eV"` | `1.602176634e-19 * RootedIn<Joule>()` | [Electronvolt](https://en.wikipedia.org/wiki/Electronvolt) |
| `Erg` | NonStandard | `Units.NonStandard.Energy` | `"erg"` | `1e-7 * RootedIn<Joule>()` (CGS) | [Erg](https://en.wikipedia.org/wiki/Erg) |

Note: kWh is implicitly available as `Kilo<Watt> * Hour` — no explicit unit needed.

---

## Force

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `KilogramForce` | NonStandard | `Units.NonStandard.Force` | `"kgf"` | `9.80665 * RootedIn<Newton>()` | [Kilogram-force](https://en.wikipedia.org/wiki/Kilogram-force) |
| `Poundal` | Imperial | `Units.Imperial.Force` | `"pdl"` | `0.138254954376 * RootedIn<Newton>()` | [Poundal](https://en.wikipedia.org/wiki/Poundal) |
| `Dyne` | NonStandard | `Units.NonStandard.Force` | `"dyn"` | `1e-5 * RootedIn<Newton>()` (CGS) | [Dyne](https://en.wikipedia.org/wiki/Dyne) |

---

## Acceleration

Target project: `Atmoos.Quantities.Units`

| Unit | System | Namespace | Symbol | Conversion | Wikipedia |
|------|--------|-----------|--------|------------|-----------|
| `StandardGravity` | NonStandard | `Units.NonStandard.Acceleration` | `"g₀"` | `9.80665 * RootedIn<Metre>() / ...` — see note | [Standard gravity](https://en.wikipedia.org/wiki/Standard_gravity) |
| `Gal` | NonStandard | `Units.NonStandard.Acceleration` | `"Gal"` | `0.01 * RootedIn<Metre>() / ...` (= cm/s²) | [Gal (unit)](https://en.wikipedia.org/wiki/Gal_(unit)) |

Note: Acceleration is a compound quantity (L·T⁻²). `StandardGravity` and `Gal` therefore need to be defined as compound units. Verify the mechanism used by existing compound units (e.g. `Knot` in `NonStandard.Velocity`) before implementing.

---

## Power

Note: BTU/h is implicitly available as `BritishThermalUnit / Hour` — no explicit unit needed.

---

## Suggested Implementation Order

1. **Length**: Fathom, League, Thou — straightforward imperial chain from existing `Foot`/`Inch`/`Mile`
2. **Mass**: TroyOunce, Carat — high practical value (metals/gemstones)
3. **Energy**: Calorie, BritishThermalUnit — widely used in everyday and engineering contexts
4. **Pressure**: MmHg, InHg — medical and aviation use
5. **Time**: Year — most broadly needed
6. **Force**: KilogramForce — bridges SI and everyday weight language
7. **Volume**: Barrel, Bushel — common in commodity/trade contexts
8. **Acceleration**: StandardGravity, Gal — physics/geophysics
9. **Mass**: Dalton, TroyPound — specialised domains
10. **Force**: Poundal, Dyne — legacy/CGS
11. **Energy**: Electronvolt, Erg — physics/CGS
12. **Length**: Parsec, Hand, Point — astronomical/specialised
13. **Time**: Fortnight — niche
14. **Pressure**: TechnicalAtmosphere — engineering niche
