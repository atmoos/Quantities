using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Pint
public readonly struct Pint : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double toCubicMetre = 0.56826125e-3; // pt -> m³ 
    public static Transformation ToSi(Transformation self) => 0.56826125 * self / 1e3;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double pintToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(pintToCubicInch * self);
    }
    public static String Representation => "pt";
}
