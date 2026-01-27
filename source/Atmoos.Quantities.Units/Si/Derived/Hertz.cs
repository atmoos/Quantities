using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.Si.Derived;

public readonly struct Hertz : ISiUnit, IFrequency, IInvertible<ITime>
{
    public static Transformation ToSi(Transformation self) => self;

    static T ISystemInject<ITime>.Inject<T>(ISystems<ITime, T> basis) => basis.Si<Second>();

    public static String Representation => "Hz";
}
