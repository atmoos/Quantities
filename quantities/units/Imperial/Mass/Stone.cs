using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Mass;

public readonly struct Stone : IImperial, IMass
{
    private static readonly Transform transform = new(6.35029318 /* kg */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "st";
}
