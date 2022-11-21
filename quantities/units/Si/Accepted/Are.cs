using Quantities.Dimensions;

namespace Quantities.Units.Si.Accepted;

public readonly struct Are : ISiAcceptedUnit, IArea<ILength>, IInjectUnit<ILength>
{
    internal const Double ToSquareMetre = 1e2; // a -> m²
    public static Double ToSi(in Double value) => ToSquareMetre * value;
    public static Double FromSi(in Double value) => value / ToSquareMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * ToSquareMetre);
    }
    public static String Representation => "a";
}
