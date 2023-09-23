using Quantities.Dimensions;
using Quantities.Units.Si;

namespace Quantities.Units.NonStandard.Area;

// See: https://de.wikipedia.org/wiki/Morgen_(Einheit)
public readonly struct Morgen : INoSystemUnit, IArea, IAlias<ILength>
{
    internal const Double ToSquareMetre = 2500; // Mg -> m²
    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;
    static T IAlias<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();
    public static String Representation => "Mg";
}
