using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Minute : ISiAcceptedUnit, IUnitInject<ITime>, ITime
{
    internal const Double ToSeconds = 60; // min -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ITime>.Inject<T>(in IInject<ITime, T> inject, in Double self)
    {
        return inject.InjectSi<Second>(self * ToSeconds);
    }
    public static String Representation => "m";
}
public readonly struct Hour : ISiAcceptedUnit, IUnitInject<ITime>, ITime
{
    internal const Double ToSeconds = 60 * Minute.ToSeconds; // Hour -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ITime>.Inject<T>(in IInject<ITime, T> inject, in Double self)
    {
        return inject.InjectSi<Second>(self * ToSeconds);
    }
    public static String Representation => "h";
}
public readonly struct Day : ISiAcceptedUnit, IUnitInject<ITime>, ITime
{
    internal const Double ToSeconds = 24 * Hour.ToSeconds; // Day -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ITime>.Inject<T>(in IInject<ITime, T> inject, in Double self)
    {
        return inject.InjectSi<Second>(self * ToSeconds);
    }
    public static String Representation => "d";
}
public readonly struct Week : ISiAcceptedUnit, IUnitInject<ITime>, ITime
{
    internal const Double ToSeconds = 7 * Day.ToSeconds; // Week -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IUnitInject<ITime>.Inject<T>(in IInject<ITime, T> inject, in Double self)
    {
        return inject.InjectSi<Second>(self * ToSeconds);
    }
    public static String Representation => "w";
}
