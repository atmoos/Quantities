using Quantities.Dimensions;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Litre : ISiAcceptedUnit, IVolume<ILength>, IUnitInject<ILength>
{
    internal const Double InCubicMetre = 1e3; // 1000 ℓ in 1 m³
    public static Double ToSi(in Double value) => value / InCubicMetre;
    public static Double FromSi(in Double value) => value * InCubicMetre;
    static T IUnitInject<ILength>.Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        return inject.InjectSi<Metre>(self / InCubicMetre);
    }
    public static String Representation => "ℓ";
}
