using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Area;

public readonly struct Acre : IImperial, IArea<ILength>, IUnitInject<ILength>
{
    private static readonly Transform transform = new(4046.8564224 /* m² */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);

    // one acre is 4'840 square yards
    public static T Inject<T>(in IInject<ILength, T> inject, in Double self) => inject.Inject<Yard>(4840 * self);
    public static String Representation => "ac";
}
