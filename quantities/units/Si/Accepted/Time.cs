using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Minute : ISiAcceptedUnit, ITime
{
    private static readonly Transform transform = new(60 /* s */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "m";
}
public readonly struct Hour : ISiAcceptedUnit, ITime
{
    private static readonly Transform transform = new(60 * 60 /* s */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "h";
}
public readonly struct Day : ISiAcceptedUnit, ITime
{
    private static readonly Transform transform = new(60 * 60 * 24 /* s */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "d";
}
public readonly struct Week : ISiAcceptedUnit, ITime
{
    private static readonly Transform transform = new(60 * 60 * 24 * 7 /* s */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "w";
}
