﻿using Quantities.Dimensions;
using Quantities.Units.Transformation;

namespace Quantities.Units.Imperial.Volume;

public readonly struct Quart : IImperialUnit, IVolume<ILength>
{
    private static readonly Transform transform = new(1.1365225e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static String Representation => "qt";
}
