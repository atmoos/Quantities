using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Zentner
public readonly struct Zentner : INonStandardUnit, IMass
{
    public static Transformation ToSi(Transformation self) => 50 * self.RootedIn<Kilogram>();

    public static String Representation => "Ztr";
}
