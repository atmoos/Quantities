using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Quart
public readonly struct Quart : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    private const Double toCubicMetre = 1.1365225e-3;
    private static readonly Transform transform = new(toCubicMetre /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double quartToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(quartToCubicInch * self);
    }
    public static String Representation => "qt";
}
