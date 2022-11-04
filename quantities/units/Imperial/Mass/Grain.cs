using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Mass;

public readonly struct Grain : IImperial, IMass
{
    private static readonly Transform transform = new(64.79891e-6 /* kg */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "gr";
}
