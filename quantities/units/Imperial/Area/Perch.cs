using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Area;

public readonly struct Perch : IImperial, IArea<ILength>
{
    private static readonly Transform transform = new(25.29285264 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "perch";
}
