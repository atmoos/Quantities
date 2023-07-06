using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Area;

// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Perch : IImperialUnit, IArea, IInjectUnit<ILength>
{
    private static readonly Transform transform = new(25.29285264 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Imperial<Length.Rod>(in self); // one perch is defined as one square rod...
    }
    public static String Representation => "perch";
}
