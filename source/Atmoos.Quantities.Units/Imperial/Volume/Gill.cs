using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gill_(unit)
public readonly struct Gill : IImperialUnit, IVolume, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 1.420653125 * self / 1e4;

    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Inch>();

    public static String Representation => "gi";
}
