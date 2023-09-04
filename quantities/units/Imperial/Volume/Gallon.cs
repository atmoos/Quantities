using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gallon
public readonly struct Gallon : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double toCubicMetre = 4.54609e-3; // gal -> m³
    public static Transformation ToSi(Transformation self) => 4.54609 * self / 1e3;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double gallonToCubicFeet = toCubicMetre / (Foot.ToMetre * Foot.ToMetre * Foot.ToMetre);
        return inject.Imperial<Foot>(gallonToCubicFeet * self);
    }
    public static String Representation => "gal";
}
