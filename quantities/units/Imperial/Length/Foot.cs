using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Length;

public readonly struct Foot : IImperialUnit, ILength
{
    internal const Double ToMetre = 0.3048; // ft -> m
    private static readonly Transform transform = new(ToMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "ft";
}
