using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Length;

public readonly struct Chain : IImperialUnit, ILength
{
    private static readonly Transform transform = new(20.1168 /* m */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "ch";
}
