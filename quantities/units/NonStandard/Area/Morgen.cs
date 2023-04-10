using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Area;

// See: https://de.wikipedia.org/wiki/Morgen_(Einheit)
public readonly struct Morgen : INoSystem, IArea<ILength>, IInjectUnit<ILength>
{
    internal const Double ToSquareMetre = 2500; // Mg -> m²
    public static Double ToSi(in Double value) => ToSquareMetre * value;
    public static Double FromSi(in Double value) => value / ToSquareMetre;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * ToSquareMetre);
    }
    public static String Representation => "Mg";
}
