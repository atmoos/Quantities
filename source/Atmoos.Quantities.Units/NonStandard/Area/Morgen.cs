using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Units.NonStandard.Area;

// See: https://de.wikipedia.org/wiki/Morgen_(Einheit)
public readonly struct Morgen : INonStandardUnit, IArea, IPowerOf<ILength>
{
    internal const Double ToSquareMetre = 2500; // Mg -> m²

    public static Transformation ToSi(Transformation value) => ToSquareMetre * value;

    static T ISystemInject<ILength>.Inject<T>(ISystems<ILength, T> basis) => basis.Si<Metre>();

    public static String Representation => "Mg";
}
