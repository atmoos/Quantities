using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Gill : IImperial, IVolume<ILength>
{
    private static readonly Transform transform = new(0.1420653125e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "gi";
}
