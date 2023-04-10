using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

public readonly struct FluidOunce : IImperial, IVolume<ILength>, IInjectUnit<ILength>
{
    internal const Double ToCubicMeter = 0.0284130625e-3; // fl oz -> m³
    private static readonly Transform transform = new(ToCubicMeter);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double fluidOunceToCubicInch = ToCubicMeter / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(fluidOunceToCubicInch * self);
    }
    public static String Representation => "fl oz";
}
