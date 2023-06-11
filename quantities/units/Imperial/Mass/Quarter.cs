using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Quarter : IImperialUnit, IMass
{
    private static readonly Transform transform = new(12.70058636 /* kg */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "qr";
}
