# How To

## Create a Quantity

Currently this is not possible and it likely never will be. If you need a new quantity, please fork the repository, implement your quantity in [Quantities](../source/Quantities/Quantities/) and then send a PR our way :-)

## Create Custom Units

There are two approaches to creating units. Let's illustrate this using a unit of velocity, the `NauticalMilePerLunarYear`. For the sake of simplicity, let's further not worry too much about the definition of a "LunarYear" and agree that it's 28 days.

Initially one might go about defining said unit without much thought as:

```csharp
public readonly struct NauticalMilePerLunarYear : INonStandardUnit, IVelocity
{
    public static Transformation ToSi(Transformation self)
    {
        var nauticalMiles = 1852 * self.RootedIn<Metre>();
        return nauticalMiles / (3600 * 24 * 28);
    }

    public static String Representation => "nmi/lny";
}
```

Which would be a valid approach for a "non-compound" (scalar) unit. Notice, how the unit `NauticalMilePerLunarYear` is composed of `NauticalMile` *per* `LunarYear`, two "scalar" units of length and time.

Since the `NauticalMile` is already defined [here](../source/Quantities.Units/NonStandard/Length/NauticalMile.cs) in the [Quantities.Units](../source/Quantities.Units/) library, we only need to actually define the `LunarYear` as a non-standard unit of time.

```csharp
public readonly struct LunarYear : INonStandardUnit, ITime
{
    public static Transformation ToSi(Transformation self)
    {
        return 28 * self.DerivedFrom<Day>();
    }

    public static String Representation => "lny";
}
```

Now we can use the `LunarYear` for both quantities of time, velocity, acceleration or even energy.

Both units can be used to create identical quantities:

```csharp
Velocity threeNmLy_1 = Velocity.Of(3, NonStandard<NauticalMilePerLunarYear>());
Velocity threeNmLy_2 = Velocity.Of(3, NonStandard<NauticalMile>().Per(NonStandard<LunarYear>()));
```

## Use Frequently Used Compound Units Elegantly

The example above is a nice example as to how unwieldy some units can become. If a particular code base uses the same set of units over and over, these can (and should?) be defined in a static class to reflect the usage in a particular domain:

```csharp
public static class MyCommonlyUsedUnits
{
    public static Product<Watt, Hour> kWh { get; } = Si<Kilo, Watt>().Times(Metric<Hour>());
    public static Quotient<NauticalMile, LunarYear> NauticalMilePerLunarYear { get; } = NonStandard<NauticalMile>().Per(NonStandard<LunarYear>());

    /* more */
}
```

Usage would then look something like this:

```csharp
using static Quantities.Units.MyCommonlyUsedUnits;

/* snip */

Energy fourKiloWattHours = Energy.Of(4, kWh);

Velocity threeNmLy = Velocity.Of(3, NauticalMilePerLunarYear);
```
