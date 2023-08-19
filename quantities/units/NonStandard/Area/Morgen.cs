using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Area;

// See: https://de.wikipedia.org/wiki/Morgen_(Einheit)
public readonly struct Morgen : INoSystemUnit, IArea, IInjectUnit<ILength>
{
    internal const Double ToSquareMetre = 2500; // Mg -> m²
    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;
    static T IInjectUnit<ILength>.Inject<T>(in Creator<ILength, T> inject, in Double self)
    {
        return inject.Si<Metre>(self * ToSquareMetre);
    }
    public static String Representation => "Mg";
}
