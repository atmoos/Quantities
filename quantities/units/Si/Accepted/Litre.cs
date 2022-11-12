using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Litre : ITransform, ISiAcceptedUnit, IVolume<ILength>, IUnitInject<ILength>
{
    private static readonly Transform transform = new(1e-3  /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static T Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        // ToDo!
        return inject.Inject<Foot>(1e-3 * self);
    }
    public static String Representation => "ℓ";
}
