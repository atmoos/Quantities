using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Length;

public readonly struct Foot : IImperial, ILength
{
    private static readonly Transform transform = new(0.3048 /* m */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "ft";
}
