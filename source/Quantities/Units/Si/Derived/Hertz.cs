using Quantities.Dimensions;

namespace Quantities.Units.Si.Derived;
public readonly struct Hertz : ISiUnit, IFrequency
{
    public static Transformation ToSi(Transformation self) => self;
    public static String Representation => "Hz";
}
