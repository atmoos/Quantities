## Plan: Fix Unit Inconsistencies

**TL;DR** — All unit definitions in the `Atmoos.Quantities` and `Atmoos.Quantities.Units` projects were checked for correctness and consistency. 1 conversion bug and 7 representation/semantic issues were found.

---

### Issue 1: Chain conversion factor is wrong

[Chain.cs](../../source/Atmoos.Quantities.Units/Imperial/Length/Chain.cs): `22 * self.DerivedFrom<Foot>()` yields 22 feet, but 1 chain = **66 feet** (= 22 yards). This is confirmed by [Rod.cs](../../source/Atmoos.Quantities.Units/Imperial/Length/Rod.cs) which correctly computes 16.5 feet per rod, and 1 chain = 4 rods = 66 feet.

**Fix**: Change to `66 * self.DerivedFrom<Foot>()`.

### Issue 2: Minute representation clashes with Metre (`"m"`)

[Minute.cs](../../source/Atmoos.Quantities/Units/Si/Metric/Minute.cs) uses `"m"`, which is the same as [Metre.cs](../../source/Atmoos.Quantities/Units/Si/Metre.cs). The standard abbreviation for minute is `"min"`.

**Fix**: Change Minute's representation to `"min"`.

### Issue 3: ~~Stere representation clashes with Stone (`"st"`)~~ DON'T FIX "st" is the german symbol

[Stere.cs](../../source/Atmoos.Quantities.Units/Si/Metric/Stere.cs) and [Stone.cs](../../source/Atmoos.Quantities.Units/Imperial/Mass/Stone.cs) both use `"st"`. Both are legitimate uses historically, but this creates an ambiguity.

**Fix**: Decide on one of:
- Change Stere to `"stère"` (its French origin)
- Keep `"st"` for Stone (more widely used) and use an alternative for Stere

### Issue 4: ~~Ton representation clashes with Tonne (`"t"`)~~ FIXED MANUALLY

[Ton.cs](../../source/Atmoos.Quantities.Units/Imperial/Mass/Ton.cs) and [Tonne.cs](../../source/Atmoos.Quantities.Units/Si/Metric/Tonne.cs) both use `"t"`. The metric tonne `"t"` is standardised by SI. The imperial long ton should be distinguished.

**Fix**: Change imperial Ton's representation to `"tn"`.

### Issue 5: Nibble representation clashes with Newton (`"N"`)

[Nibble.cs](../../source/Atmoos.Quantities.Units/Si/Metric/UnitsOfInformation/Nibble.cs) and [Newton.cs](../../source/Atmoos.Quantities.Units/Si/Derived/Newton.cs) both use `"N"`. Newton's `"N"` is standardised by SI and must not change. The nibble has no standard symbol.

**Fix**: Change Nibble's representation to `"nib"`.

### Issue 6: Metric HorsePower representation clashes with Imperial HorsePower (`"hp"`)

[Si/Metric/HorsePower.cs](../../source/Atmoos.Quantities.Units/Si/Metric/HorsePower.cs) and [Imperial/Power/HorsePower.cs](../../source/Atmoos.Quantities.Units/Imperial/Power/HorsePower.cs) both use `"hp"`. The metric horsepower is conventionally abbreviated `"PS"` (from German _Pferdestärke_).

**Fix**: Change metric HorsePower's representation to `"PS"`.

### Issue 7: Metric HorsePower roots in wrong SI unit

[Si/Metric/HorsePower.cs](../../source/Atmoos.Quantities.Units/Si/Metric/HorsePower.cs): `self.RootedIn<Metre>()` — `RootedIn` is functionally a no-op, so the numeric result (735.49875 W) is correct. However, an `IPower` unit should semantically root in `Watt`, not `Metre`.

**Fix**: Change `self.RootedIn<Metre>()` to `self.RootedIn<Watt>()` and add the appropriate `using` for the `Watt` type.

### Issue 8: Week abbreviation is non-standard

[Week.cs](../../source/Atmoos.Quantities.Units/Si/Metric/Week.cs) uses `"w"`. The accepted abbreviation for week is `"wk"`.

**Fix**: Change Week's representation to `"wk"`.

---

### Steps

1. **Fix Issue 1** — In [Chain.cs](../../source/Atmoos.Quantities.Units/Imperial/Length/Chain.cs): change `22` to `66` in the `ToSi` method. Update any corresponding tests.

2. **Fix Issue 2** — In [Minute.cs](../../source/Atmoos.Quantities/Units/Si/Metric/Minute.cs): change `Representation` from `"m"` to `"min"`. Update any corresponding tests.

3. **Fix Issue 5** — In [Nibble.cs](../../source/Atmoos.Quantities.Units/Si/Metric/UnitsOfInformation/Nibble.cs): change `Representation` from `"N"` to `"nib"`. Update any corresponding tests.

4. **Fix Issue 6** — In [Si/Metric/HorsePower.cs](../../source/Atmoos.Quantities.Units/Si/Metric/HorsePower.cs): change `Representation` from `"hp"` to `"PS"`. Update any corresponding tests.

5. **Fix Issue 7** — In [Si/Metric/HorsePower.cs](../../source/Atmoos.Quantities.Units/Si/Metric/HorsePower.cs): change `self.RootedIn<Metre>()` to `self.RootedIn<Watt>()` and add `using Atmoos.Quantities.Units.Si.Derived;`.

6. **Fix Issue 8** — In [Week.cs](../../source/Atmoos.Quantities.Units/Si/Metric/Week.cs): change `Representation` from `"w"` to `"wk"`. Update any corresponding tests.

7. **Run all tests** to ensure no regressions. Fix any test failures that arise from the above changes.
