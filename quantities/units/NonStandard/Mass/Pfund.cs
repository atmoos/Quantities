using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Mass;

// https://de.wikipedia.org/wiki/Pfund
public readonly struct Pfund : INoSystemUnit, IMass
{
    public static Transformation ToSi(Transformation self) => self.RootedIn<Kilogram>() / 2;
    public static String Representation => "℔";
}
