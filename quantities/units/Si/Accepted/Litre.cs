using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Litre : ISiAcceptedUnit, IVolume<ILength>, IUnitInject<ILength>
{
    internal const Double ToCubicMetre = 1e-3; // ℓ -> m³
    private static readonly Transform transform = new(ToCubicMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ILength>.Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        // ToDo!
        return inject.Inject<Foot>(1e-3 * self);
    }
    public static String Representation => "ℓ";
}
