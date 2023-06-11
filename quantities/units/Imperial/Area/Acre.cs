using Quantities.Dimensions;
using Quantities.Units.Imperial.Length;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Area;

public readonly struct Acre : IImperialUnit, IArea<ILength>, IInjectUnit<ILength>
{
    internal const Double ToSquareMetre = 4046.8564224; // ac -> m²
    private static readonly Transform transform = new(ToSquareMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        const Double acreToSqYard = 4840;
        return inject.Imperial<Yard>(acreToSqYard * self);
    }
    public static String Representation => "ac";
}
