using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Area;

public readonly struct Acre : IImperialUnit, IArea, IInjectUnit<ILength>
{
    public static Transformation ToSi(Transformation self) => 4046.8564224 * self;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double acreToSqYard = 4840;
        return inject.Imperial<Yard>(acreToSqYard * self);
    }
    public static String Representation => "ac";
}
