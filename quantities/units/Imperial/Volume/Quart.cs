using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Quart : IImperial, IVolume<ILength>
{
    private static readonly Transform transform = new(1.1365225e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "qt";
}
