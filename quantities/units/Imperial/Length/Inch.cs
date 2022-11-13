﻿using Quantities.Dimensions;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Length;

public readonly struct Inch : IImperial, ILength
{
    internal const Double ToMetre = 0.0254; // in -> m
    private static readonly Transform transform = new(ToMetre);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "in";
}
