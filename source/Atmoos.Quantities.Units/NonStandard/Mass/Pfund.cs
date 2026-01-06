using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Pfund
public readonly struct Pfund : INonStandardUnit, IMass
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Kilogram>() / 2;

    public static String Representation => "℔";
}
