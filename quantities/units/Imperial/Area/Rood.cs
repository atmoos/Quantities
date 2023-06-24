using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Area;

public readonly struct Rood : IImperialUnit, IArea, IInjectUnit<ILength>
{
    private static readonly Transform transform = new(1011.7141056 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double roodToSqYard = 1210;
        return inject.Imperial<Length.Yard>(roodToSqYard * self);
    }
    public static String Representation => "rōd";
}
