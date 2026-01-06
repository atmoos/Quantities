using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Area;

// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Perch : IImperialUnit, IArea, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 25.29285264 * self;

    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Rod>();

    public static String Representation => "perch";
}
