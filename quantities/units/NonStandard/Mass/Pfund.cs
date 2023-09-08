using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Pfund
public readonly struct Pfund : INoSystemUnit, IMass
{
    internal const Double toKilogram = 0.5; // ℔ -> Kg
    public static Transformation ToSi(Transformation self) => toKilogram * self;
    public static String Representation => "℔";
}
