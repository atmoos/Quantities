using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

// https://en.wikipedia.org/wiki/Rod_(unit)
// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Rod : IImperialUnit, ILength
{
    public static Transformation ToSi(Transformation self) => 5.0292 * self;
    public static String Representation => "rod";
}
