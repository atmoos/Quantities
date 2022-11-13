using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Pint : IImperial, IVolume<ILength>, IUnitInject<ILength>
{
    internal const Double ToCuMetre = 0.56826125e-3; // pt -> m³ 
    private static readonly Transform transform = new(ToCuMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ILength>.Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        const Double ptTuCuIn = ToCuMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Inject<Inch>(ptTuCuIn * self);
    }
    public static String Representation => "pt";
}
