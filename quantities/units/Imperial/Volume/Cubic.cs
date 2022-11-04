using Quantities.Dimensions;

namespace Quantities.Unit.Imperial.Volume;

// ToDo: Add IImperial to generic constraints again.
public readonly struct Cubic<TUnit> : IImperial, IVolume<TUnit>
    where TUnit : IUnit, ITransform, ILength, new()
{
    // ToDo: This could make use of the Transform struct...
    private static readonly Double toSi = Math.Pow(TUnit.ToSi(1d), 3);
    private static readonly Double fromSi = Math.Pow(TUnit.FromSi(1d), 3);
    public static Double FromSi(in Double siValue) => siValue * fromSi;
    public static Double ToSi(in Double nonSiValue) => nonSiValue * toSi;
    public static String Representation { get; } = $"{TUnit.Representation}³";
}
