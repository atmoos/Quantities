using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Area;

public readonly struct Acre : IImperial, IArea<ILength>, IUnitInject<ILength>
{
    internal const Double ToSquareMetre = 4046.8564224; // ac -> m²
    private static readonly Transform transform = new(ToSquareMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ILength>.Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        const Double acreToSqYard = 4840;
        return inject.Inject<Yard>(acreToSqYard * self);
    }
    public static String Representation => "ac";
}
