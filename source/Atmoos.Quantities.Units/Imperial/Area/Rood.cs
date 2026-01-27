using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Imperial.Length;

namespace Atmoos.Quantities.Units.Imperial.Area;

public readonly struct Rood : IImperialUnit, IArea, IPowerOf<ILength>
{
    public static Transformation ToSi(Transformation self) => 1011.7141056 * self;

    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Imperial<Yard>();

    public static String Representation => "rōd";
}
