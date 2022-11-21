using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Area;

public readonly struct Rood : IImperial, IArea<ILength>
{
    private static readonly Transform transform = new(1011.7141056 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "rōd";
}
