using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct FluidOunce : IImperial, IVolume<ILength>, IUnitInject<ILength>
{
    internal const Double ToCuMeter = 0.0284130625e-3; // fl oz -> m³
    private static readonly Transform transform = new(ToCuMeter);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ILength>.Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        const Double flOzToCuIn = ToCuMeter / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Inject<Inch>(flOzToCuIn * self);
    }
    public static String Representation => "fl oz";
}
