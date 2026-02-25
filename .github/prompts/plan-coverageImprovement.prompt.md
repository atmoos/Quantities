# Plan: Improve Line & Branch Coverage

**TL;DR** — Current overall coverage is 85.6% line / 69.8% branch. Five classes were identified as having the highest potential to improve coverage using only the public API (no `InternalsVisibleTo` tricks). A simplified plan for three honourable mentions follows at the end.

**Constraint**: All tests must use the **public API only** — no `internal` access, no reflection hacks.

---

## Target 1: `Kinematics` — 47.3% line coverage (19 operators, ~5 tested)

**Source**: [MechanicalEngineering.cs](../../source/Atmoos.Quantities/Physics/MechanicalEngineering.cs#L29-L96)

**File**: New test file `Atmoos.Quantities.Units.Test/KinematicsTest.cs` (or `KinematicsTest.ai.cs`)

`Kinematics` defines 19 cross-quantity operators. Only ~5 are exercised by existing tests (mostly via `VelocityTest`, `AccelerationTest`, `ForceTest`, `EnergyTest`). The remaining 14 operators have zero coverage and are all directly testable through the public API.

### Tests to add

Each operator is a single `[Fact]` test. Create quantities in known units, apply the operator, and assert the result matches a directly-created expected value.

| # | Operator | Test name |
|---|----------|-----------|
| 1 | `Length / Velocity → Time` | `LengthDividedByVelocityYieldsTime` |
| 2 | `Velocity * Time → Length` | `VelocityTimesTimeYieldsLength` |
| 3 | `Velocity / Acceleration → Time` | `VelocityDividedByAccelerationYieldsTime` |
| 4 | `Velocity * Force → Power` | `VelocityTimesForceYieldsPower` |
| 5 | `Area * Pressure → Force` | `AreaTimesPressureYieldsForce` |
| 6 | `Time * Power → Energy` | `TimeTimesPowerYieldsEnergy` |
| 7 | `Time * Velocity → Length` | `TimeTimesVelocityYieldsLength` |
| 8 | `Time * Acceleration → Velocity` | `TimeTimesAccelerationYieldsVelocity` |
| 9 | `Force / Area → Pressure` | `ForceDividedByAreaYieldsPressure` |
| 10 | `Force * Velocity → Power` | `ForceTimesVelocityYieldsPower` |
| 11 | `Power / Force → Velocity` | `PowerDividedByForceYieldsVelocity` |
| 12 | `Power / Velocity → Force` | `PowerDividedByVelocityYieldsForce` |
| 13 | `Power * Time → Energy` | `PowerTimesTimeYieldsEnergy` |
| 14 | `Energy / Time → Power` | `EnergyDividedByTimeYieldsPower` |
| 15 | `Energy / Power → Time` | `EnergyDividedByPowerYieldsTime` |

**Pattern** (example):

```csharp
[Fact]
public void LengthDividedByVelocityYieldsTime()
{
    Length distance = Length.Of(100, Si<Metre>());
    Velocity speed = Velocity.Of(10, Si<Metre>().Per(Si<Second>()));
    Time expected = Time.Of(10, Si<Second>());

    Time actual = distance / speed;

    Assert.Equal(expected, actual);
}
```

### Impact

15 new tests → covers all 19 operators (14 new + 1 duplicated for clarity). Expect to go from ~47% to ~95%+ line coverage on `Kinematics`.

---

## Target 2: `Extensions` — 50.0% line coverage

**Source**: [Extensions.cs](../../source/Atmoos.Quantities/Extensions.cs)

**File**: Extend existing [ExtensionsTest.cs](../../source/Atmoos.Quantities.Test/ExtensionsTest.cs)

The existing tests only cover `Deconstruct`. Several public methods have zero coverage: `ValueOf<T>`, `ToString(IFormattable, String)`, `NotImplemented(Object)`, and `NotImplemented<T>()`.

### Tests to add

| # | Method | Test name | What to assert |
|---|--------|-----------|----------------|
| 1 | `ValueOf<T>()` | `ValueOfReturnsConversionFactor` | `ValueOf<Kilo>()` equals `1000.0` |
| 2 | `ValueOf<T>(exponent)` | `ValueOfWithExponentReturnsScaledFactor` | `ValueOf<Kilo>(2)` equals `1_000_000.0` |
| 3 | `ValueOf<T>(exponent)` | `ValueOfWithNegativeExponentReturnsInverse` | `ValueOf<Kilo>(-1)` is close to `0.001` |
| 4 | `ToString(IFormattable, String)` | `ToStringUsesInvariantCulture` | A quantity's `ToString("G4")` returns a culture-invariant string |
| 5 | `NotImplemented(Object)` | `NotImplementedContainsTypeName` | Exception message contains the type name |
| 6 | `NotImplemented<T>()` | `GenericNotImplementedContainsTypeName` | Exception message contains `T`'s name |
| 7 | `Deconstruct` (edge case) | `DeconstructionOfNegativeValuePreservesSign` | Negative quantity deconstructs correctly |

### Impact

7 new tests → expect to go from ~50% to ~90%+ line coverage on `Extensions`.

---

## Target 3: `QuantityConverter<T>` (Newtonsoft) — 67.6% line coverage

**Source**: [QuantityConverter.cs](../../source/Atmoos.Quantities.Serialization/Newtonsoft/QuantityConverter.cs)

**File**: New per-quantity support test files in `Newtonsoft.Test/`, mirroring the Text.Json.Test structure.

The Text.Json side has 17 dedicated `*SupportTest.cs` files achieving 97% coverage on its `QuantityConverter<T>`. The Newtonsoft side has only one monolithic `SerializationTest.cs` with 8 tests and no compound quantity roundtrips. The fix is to mirror the Text.Json test structure.

### Tests to add

Create the following `*SupportTest.cs` files in `Newtonsoft.Test/`, each following the pattern of the Text.Json equivalents (using the Newtonsoft `Convenience.SupportsSerialization` helper):

| # | New test file | Quantities exercised | Key coverage paths |
|---|---------------|---------------------|--------------------|
| 1 | `VelocitySupportTest` | Velocity (SI, metric, imperial, non-standard) | `ReadMany`/compound deserialization, `ProductInjector` with `(+,-)` exponents |
| 2 | `EnergySupportTest` | Energy (Joule, kWh, MWs) | `ReadMany` with `(+,+)` exponents, alias serialization |
| 3 | `PowerSupportTest` | Power (Watt, HorsePower) | Named unit serialization |
| 4 | `AccelerationSupportTest` | Acceleration (m/s²) | Exponent handling in compound quantities |
| 5 | `ForceSupportTest` | Force (Newton, PoundForce) | Imperial compound serialization |
| 6 | `PressureSupportTest` | Pressure (Pascal, Bar, Atm, Torr) | NonStandard + metric unit serialization |
| 7 | `FrequencySupportTest` | Frequency (Hertz) | Invertible unit (exponent -1) serialization |
| 8 | `TemperatureSupportTest` | Temperature (Celsius, Fahrenheit, Kelvin) | Non-linear transforms in serialization |
| 9 | `AreaSupportTest` | Area (m², alias via Are/Acre) | Alias power unit serialization |
| 10 | `VolumeSupportTest` | Volume (m³, Litre, Gallon) | Higher-order alias power unit serialization |
| 11 | `TimeSupportTest` | Time (s, min, h, day, week) | Metric unit serialization variety |
| 12 | `MassSupportTest` | Mass (kg, g, lb, Stone) | Imperial mass serialization |
| 13 | `DataSupportTest` | Data (Byte, Bit, with binary prefixes) | Binary prefix serialization |
| 14 | `DataRateSupportTest` | DataRate (Byte/s, Bit/s) | Binary prefix + compound serialization |
| 15 | `ElectricCurrentSupportTest` | ElectricCurrent (A) | Base SI serialization |
| 16 | `ElectricPotentialSupportTest` | ElectricPotential (V) | Derived SI serialization |
| 17 | `ElectricalResistanceSupportTest` | ElectricalResistance (Ω) | Derived SI serialization |

Also add a convenience `ISerializationTester<T>` interface to `Newtonsoft.Test/` (mirroring the Text.Json one).

**Pattern** (example):

```csharp
public class VelocitySupportTest : ISerializationTester<Velocity>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Velocity velocity) => velocity.SupportsSerialization();

    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Velocity> Interesting()
        {
            yield return Velocity.Of(21, Si<Metre>().Per(Si<Second>()));
            yield return Velocity.Of(342, Si<Kilo, Metre>().Per(Metric<Hour>()));
            yield return Velocity.Of(6, Imperial<Mile>().Per(Metric<Hour>()));
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
```

### Impact

17 new test files with ~3–7 data points each → expect to go from ~68% to ~95%+ on Newtonsoft's `QuantityConverter<T>`, and also improves coverage on `ScalarBuilder` (target 4) and `ProductInjector` (honourable mention).

---

## Target 4: `ScalarBuilder` — 81.8% line / 31.8% branch coverage

**Source**: [ScalarBuilder.cs](../../source/Atmoos.Quantities/Core/Construction/ScalarBuilder.cs)

`ScalarBuilder` is internal, but every branch is exercised through serialization roundtrips. Each `CreateXYZ` method variant corresponds to a (system × prefix × alias/invertible) combination. The uncovered branches are paths not yet hit by serialization test data.

### How to cover via Target 3

The Newtonsoft support tests above (Target 3) already cover most paths, provided the test data includes:

| Missing path | Required test data |
|--------------|--------------------|
| `CreateMetric` (no prefix) | `Time.Of(v, Metric<Hour>())` — in `TimeSupportTest` |
| `CreateMetricAlias` | `Volume.Of(v, Metric<Litre>())` — in `VolumeSupportTest` |
| `CreateImperialAlias` | `Area.Of(v, Imperial<Acre>())` — in `AreaSupportTest` |
| `CreateNonStandardInvertible` | N/A — no existing non-standard invertible units |
| `CreateSiInvertible` | `Frequency.Of(v, Si<Hertz>())` — in `FrequencySupportTest` |
| `CreateSiInvertible` with prefix | `Frequency.Of(v, Si<Kilo, Hertz>())` — in `FrequencySupportTest` |
| `CreateImperial` (no prefix) | `Length.Of(v, Imperial<Mile>())` — in existing tests |
| `CreateNonStandard` | `Velocity.Of(v, NonStandard<Knot>())` — in `VelocitySupportTest` |

### Additional test in `Newtonsoft.Test/SerializationTest.cs`

| # | Test name | What it covers |
|---|-----------|----------------|
| 1 | `InvertibleUnitsAreSupported` | Frequency with `Si<Hertz>()` roundtrip |
| 2 | `CompoundQuantityRoundTrip` | Velocity with mixed systems roundtrip |

### Impact

These tests map directly to `ScalarBuilder`'s `GetMethod` dispatch and the `GetUnitTypeArgs` alias/invertible detection. Combined with Target 3, expect branch coverage to improve from ~32% to ~70%+.

---

## Target 5: `Introspection` — 55.5% line coverage

**Source**: [Introspection.cs](../../source/Atmoos.Quantities/Common/Introspection.cs)

`Introspection` is internal, but its branches are exercised through any quantity operation that triggers dimension/unit reflection. The uncovered paths are:

| Uncovered path | How to exercise |
|----------------|-----------------|
| `Implements(Type, Type)` — the `IsAssignableTo` short-circuit | Already covered by many tests |
| `InnerType` — error path (≠ 1 generic arg) | Cannot be triggered from public API without a malformed unit type |
| `ImplementsGeneric` — false branch | Already covered indirectly |

### Practical approach

Coverage improvements for `Introspection` are a **side-effect** of Targets 1–4. The remaining uncovered paths (~4 lines) are error guards that can only fire with malformed types. No additional dedicated tests needed, but the serialization variety in Targets 3–4 will push some currently-uncovered `InnerTypes`/`MostDerivedOf` paths from 0 to covered.

### Expected impact

From ~55% to ~70%, purely as a side-effect. The remaining ~30% are defensive error paths unreachable from the public API.

---

## Honourable Mentions

### `ProductInjector` — 63.6% line coverage

**Source**: [Injectors.cs](../../source/Atmoos.Quantities/Core/Construction/Injectors.cs)

Covered as a side-effect of Target 3 (Newtonsoft serialization tests). The missing paths correspond to the four `(leftExp, rightExp)` sign combinations during compound deserialization:

| Combination | Example quantity | Covered by |
|-------------|-----------------|------------|
| `(+, -)` | Velocity (m/s) | `VelocitySupportTest` |
| `(+, +)` | Energy (W·h) | `EnergySupportTest` |
| `(-, +)` | Not a common physical quantity | Not feasible from public API |
| `(-, -)` | Not a common physical quantity | Not feasible from public API |

**Plan**: No dedicated tests needed. The `(+,-)` and `(+,+)` paths are covered by the Newtonsoft support tests. The `(-,+)` and `(-,-)` paths cannot be reached from standard physical quantities.

### `QuantityFactory<T>` — 86.1% line coverage

**Source**: [QuantityFactory.cs](../../source/Atmoos.Quantities/Core/Construction/QuantityFactory.cs)

Partially covered by existing serialization tests. Missing paths:

| Missing path | Plan |
|--------------|------|
| Exponent -1 path (`Inject.Inverse`) | Add a `FrequencySupportTest` to Newtonsoft.Test (Target 3) with `Si<Hertz>()` |
| Exponent 3 path (`Inject.Cubic`) | Add a `VolumeSupportTest` with `Cubic(Si<Metre>())` |
| Error path for unsupported exponents | Add a test deserialising JSON with exponent > 5 and assert `NotSupportedException` |

**Plan**: These are covered as side-effects of Target 3 and Target 4. Add one dedicated error test to `Newtonsoft.Test/SerializationTest.cs`:

```csharp
[Fact]
public void UnsupportedExponentThrows()
{
    // Manually craft JSON with an unsupported exponent value (e.g. 6)
    String json = """{"value":1.0,"quantity":"length","si":{"exponent":6,"unit":"m"}}""";
    Assert.Throws<NotSupportedException>(() => json.Deserialize<Length>());
}
```

### `Dimensions.Product` — 71.7% line coverage

**Source**: [Dimension.cs](../../source/Atmoos.Quantities/Dimensions/Dimension.cs#L143-L210)

`Product` is internal, but its methods are exercised by cross-quantity operations. Missing coverage areas:

| Gap | Exercise through |
|-----|------------------|
| `Root(0)` → `DivideByZeroException` | Not reachable from public API |
| `Swap()` | Triggered by compound unit conversions |
| `ToString()` with `e ≠ 1` | Triggered by displaying higher-order compound quantities |
| `GetHashCode()` | Triggered by using compound quantities in sets/dictionaries |

**Plan**: Most gaps are covered as side-effects of Targets 1 and 3 (cross-quantity operators and compound serialization). No dedicated tests needed beyond what's already planned.

---

## Steps (execution order)

1. **Create `KinematicsTest.ai.cs`** in `Atmoos.Quantities.Units.Test/` with 15 operator tests (Target 1).

2. **Extend `ExtensionsTest.cs`** in `Atmoos.Quantities.Test/` with 7 new tests (Target 2).

3. **Create `ISerializationTester.cs`** in `Newtonsoft.Test/` mirroring the Text.Json interface.

4. **Create 17 `*SupportTest.ai.cs` files** in `Newtonsoft.Test/` (Target 3), starting with `VelocitySupportTest`, `EnergySupportTest`, `FrequencySupportTest`, `VolumeSupportTest`, and `AreaSupportTest` (these hit the most uncovered `ScalarBuilder` and `ProductInjector` paths).

5. **Add 2–3 edge-case tests** to `Newtonsoft.Test/SerializationTest.cs`: invertible unit roundtrip, compound quantity roundtrip, unsupported exponent error (Targets 3–4 + honourable mentions).

6. **Run all tests** to verify no regressions and measure improved coverage.
