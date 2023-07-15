using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Area;

// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Perch : IImperialUnit, IArea, IInjectUnit<ILength>
{
    public static Transformation ToSi(Transformation self) => 25.29285264 * self;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Imperial<Length.Rod>(in self); // one perch is defined as one square rod...
    }
    public static String Representation => "perch";
}
