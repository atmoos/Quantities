using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Pint : IImperial, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double ToCuMetre = 0.56826125e-3; // pt -> m³ 
    private static readonly Transform transform = new(ToCuMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double ptTuCuIn = ToCuMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Other<Inch>(ptTuCuIn * self);
    }
    public static String Representation => "pt";
}
