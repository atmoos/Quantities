using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Gallon : IImperial, IVolume<ILength>, IUnitInject<ILength>
{
    private static readonly Transform transform = new(4.54609e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static T Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        return inject.Inject<Foot>(4.54609e-3 / (0.3048 * 0.3048 * 0.3048) * self);
    }
    public static String Representation => "gal";
}
