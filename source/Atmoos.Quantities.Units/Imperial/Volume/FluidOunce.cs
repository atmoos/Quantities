using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Fluid_ounce
public readonly struct FluidOunce : IImperialUnit, IVolume, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 2.84130625 * self / 1e5;
    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Inch>();
    public static String Representation => "fl oz";
}
