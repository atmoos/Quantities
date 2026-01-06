using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Area;

public readonly struct Acre : IImperialUnit, IArea, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 4046.8564224 * self;

    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Yard>();

    public static String Representation => "ac";
}
