using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gill_(unit)
public readonly struct Gill : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    private const Double toCubicMetre = 0.1420653125e-3;
    public static Transformation ToSi(Transformation self) => 1.420653125 * self / 1e4;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double gillToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(gillToCubicInch * self);
    }
    public static String Representation => "gi";
}
