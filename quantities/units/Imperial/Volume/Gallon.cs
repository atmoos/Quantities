using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

public readonly struct Gallon : IImperialUnit, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double ToCubicMetre = 4.54609e-3; // gal -> m³
    private static readonly Transform transform = new(ToCubicMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double gallonToCubicFeet = ToCubicMetre / (Foot.ToMetre * Foot.ToMetre * Foot.ToMetre);
        return inject.Imperial<Foot>(gallonToCubicFeet * self);
    }
    public static String Representation => "gal";
}
