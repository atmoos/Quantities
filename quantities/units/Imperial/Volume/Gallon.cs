using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gallon
public readonly struct Gallon : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    const Double footToMetre = 0.3048;
    internal const Double toCubicMetre = 4.54609e-3; // gal -> m³
    public static Transformation ToSi(Transformation self) => 4.54609 * self / 1e3;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double gallonToCubicFeet = toCubicMetre / (footToMetre * footToMetre * footToMetre);
        return inject.Imperial<Foot>(gallonToCubicFeet * self);
    }
    public static String Representation => "gal";
}
