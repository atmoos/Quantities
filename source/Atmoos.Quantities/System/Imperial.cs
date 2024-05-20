using Atmoos.Quantities.Units.Imperial.Length;

using static Atmoos.Quantities.Systems;

namespace Atmoos.Quantities.System;

public static class Imperial
{
    public static Measure<Length> ft { get; } = new(Imperial<Foot>());
    public static Measure<Length> mi { get; } = new(Imperial<Mile>());
    public static Measure<Length> @in { get; } = new(Imperial<Inch>());

    public static Measure<Area> sqFt { get; } = new(Square(Imperial<Foot>()));
    public static Measure<Area> sqMi { get; } = new(Square(Imperial<Mile>()));
    public static Measure<Area> sqIn { get; } = new(Square(Imperial<Inch>()));

}

