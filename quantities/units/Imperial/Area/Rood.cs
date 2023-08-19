using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Area;

public readonly struct Rood : IImperialUnit, IArea, IInjectUnit<ILength>
{
    public static Transformation ToSi(Transformation self) => 1011.7141056 * self;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double roodToSqYard = 1210;
        return inject.Imperial<Length.Yard>(roodToSqYard * self);
    }
    public static String Representation => "rōd";
}
