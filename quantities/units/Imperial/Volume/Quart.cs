using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Quart
public readonly struct Quart : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    private const Double toCubicMetre = 1.1365225e-3;
    public static Transformation ToSi(Transformation self) => 1.1365225 * self / 1e3;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double quartToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(quartToCubicInch * self);
    }
    public static String Representation => "qt";
}
