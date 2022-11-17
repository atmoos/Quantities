using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Gallon : IImperial, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double ToCuMetre = 4.54609e-3; // gal -> m³
    private static readonly Transform transform = new(ToCuMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double galToCuFt = ToCuMetre / (Foot.ToMetre * Foot.ToMetre * Foot.ToMetre);
        return inject.Other<Foot>(galToCuFt * self);
    }
    public static String Representation => "gal";
}
