﻿using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Si.Accepted;

public readonly struct Minute : ISiAcceptedUnit, IInjectUnit<ITime>, ITime
{
    internal const Double ToSeconds = 60; // min -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ITime>.Inject<T>(in Creator<ITime, T> inject, in Double self)
    {
        return inject.Si<Second>(self * ToSeconds);
    }
    public static String Representation => "m";
}
public readonly struct Hour : ISiAcceptedUnit, IInjectUnit<ITime>, ITime
{
    internal const Double ToSeconds = 60 * Minute.ToSeconds; // Hour -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ITime>.Inject<T>(in Creator<ITime, T> inject, in Double self)
    {
        return inject.Si<Second>(self * ToSeconds);
    }
    public static String Representation => "h";
}
public readonly struct Day : ISiAcceptedUnit, IInjectUnit<ITime>, ITime
{
    internal const Double ToSeconds = 24 * Hour.ToSeconds; // Day -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ITime>.Inject<T>(in Creator<ITime, T> inject, in Double self)
    {
        return inject.Si<Second>(self * ToSeconds);
    }
    public static String Representation => "d";
}
public readonly struct Week : ISiAcceptedUnit, IInjectUnit<ITime>, ITime
{
    internal const Double ToSeconds = 7 * Day.ToSeconds; // Week -> s
    private static readonly Transform transform = new(ToSeconds);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    static T IInjectUnit<ITime>.Inject<T>(in Creator<ITime, T> inject, in Double self)
    {
        return inject.Si<Second>(self * ToSeconds);
    }
    public static String Representation => "w";
}
