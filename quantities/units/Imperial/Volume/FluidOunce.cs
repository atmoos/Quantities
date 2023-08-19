using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Volume;

// https://en.wikipedia.org/wiki/Fluid_ounce
public readonly struct FluidOunce : IImperialUnit, IVolume, IInjectUnit<ILength>
{
    internal const Double toCubicMetre = 0.0284130625e-3; // fl oz -> m³
    public static Transformation ToSi(Transformation self) => toCubicMetre * self;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double fluidOunceToCubicInch = toCubicMetre / (Inch.ToMetre * Inch.ToMetre * Inch.ToMetre);
        return inject.Imperial<Inch>(fluidOunceToCubicInch * self);
    }
    public static String Representation => "fl oz";
}
