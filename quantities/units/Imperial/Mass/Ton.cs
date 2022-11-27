using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Mass;

public readonly struct Ton : IImperial, IMass
{
    private static readonly Transform transform = new(1016.0469088 /* kg */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "t";
}
