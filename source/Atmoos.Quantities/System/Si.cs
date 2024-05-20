using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Metric;
using static Atmoos.Quantities.Systems;

namespace Atmoos.Quantities.System;

public static class Si
{
    # region Length
    public static Measure<Length> m { get; } = new(Si<Metre>());
    public static Measure<Length> Km { get; } = new(Si<Kilo, Metre>());
    public static Measure<Length> cm { get; } = new(Si<Centi, Metre>());
    public static Measure<Length> mm { get; } = new(Si<Milli, Metre>());
    public static Measure<Length> μm { get; } = new(Si<Micro, Metre>());
    #endregion Length

    # region Time
    public static Measure<Time> s { get; } = new(Si<Second>());
    public static Measure<Time> h { get; } = new(Metric<Hour>());
    public static Measure<Time> min { get; } = new(Metric<Minute>());
    public static Measure<Time> ms { get; } = new(Si<Milli, Second>());
    public static Measure<Time> μs { get; } = new(Si<Micro, Second>());
    #endregion Time

    # region Volume
    public static Measure<Volume> ℓ { get; } = new(Metric<Litre>());
    #endregion Volume
}

