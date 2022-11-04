using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Area;

public readonly struct Acre : IImperial, IArea<ILength>
{
    private static readonly Transform transform = new(4046.8564224 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "ac";
}
