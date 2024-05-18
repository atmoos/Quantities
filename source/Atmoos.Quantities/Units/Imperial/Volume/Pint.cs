using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Volume;

// See: https://en.wikipedia.org/wiki/Pint
public readonly struct Pint : IImperialUnit, IVolume, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 0.56826125 * self / 1e3;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Inch>();
    public static String Representation => "pt";
}
