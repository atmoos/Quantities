using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;

namespace Quantities.Units.Imperial.Area;

public readonly struct Acre : IImperialUnit, IArea, IAlias<ILength>
{
    public static Transformation ToSi(Transformation self) => 4046.8564224 * self;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Yard>();
    public static String Representation => "ac";
}
