using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Gill_(unit)
public readonly struct Gill : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    private const Double toCubicMetre = 0.1420653125e-3;
    private static readonly Transform transform = new(0.1420653125e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double gillToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(gillToCubicInch * self);
    }
    public static String Representation => "gi";
}
