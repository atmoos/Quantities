using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

public readonly struct Pint : IImperial, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double ToCubicMetre = 0.56826125e-3; // pt -> m³ 
    private static readonly Transform transform = new(ToCubicMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double ptTuCuIn = ToCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(ptTuCuIn * self);
    }
    public static String Representation => "pt";
}
