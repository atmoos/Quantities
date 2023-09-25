using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Area;

// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Perch : IImperialUnit, IArea, IAlias<ILength>
{
    public static Transformation ToSi(Transformation self) => 25.29285264 * self;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Rod>();
    public static String Representation => "perch";
}
