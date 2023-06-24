using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Length;

// https://en.wikipedia.org/wiki/Rod_(unit)
// https://en.wikipedia.org/wiki/Imperial_units
public readonly struct Rod : IImperialUnit, ILength
{
    private static readonly Transform transform = new(5.0292 /* m */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "rod";
}
