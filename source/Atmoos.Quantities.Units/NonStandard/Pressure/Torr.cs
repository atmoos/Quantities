using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Units.NonStandard.Pressure;

public readonly struct Torr : INonStandardUnit, IPressure
{
    public static Transformation ToSi(Transformation value) => value.DerivedFrom<StandardAtmosphere>() / 760;

    public static String Representation => nameof(Torr);
}
