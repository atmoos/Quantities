using Quantities.Dimensions;

namespace Quantities.Units.Imperial.Length;

public readonly struct Inch : IImperialUnit<Inch, ILength>, ILength
{
    public const Double ToMetre = 0.0254; // ToDo: Remove!
    public static Transformation Derived(in From<ILength> from) => from.Imperial<Foot>() / 12;
    public static String Representation => "in";
}
