using Quantities.Dimensions;

namespace Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Zentner
public readonly struct Zentner : INoSystemUnit, IMass
{
    internal const Double toKilogram = 50; // Ztr -> Kg
    public static Transformation ToSi(Transformation self) => toKilogram * self;
    public static String Representation => "Ztr";
}
