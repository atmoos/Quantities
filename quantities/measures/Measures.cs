using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units;
using Quantities.Units.Si;

using static System.Math;

namespace Quantities.Measures;

internal readonly struct Si<TUnit> : ISiMeasure<TUnit>, ILinear
    where TUnit : ISiBaseUnit
{
    public static Double ToSi(in Double value) => value;
    public static Double FromSi(in Double value) => value;
    public static String Representation => TUnit.Representation;
}
internal readonly struct Si<TPrefix, TUnit> : ISiMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiBaseUnit
{
    public static Double ToSi(in Double value) => TPrefix.ToSi(in value);
    public static Double FromSi(in Double value) => TPrefix.FromSi(in value);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}
internal readonly struct SiDerived<TUnit> : ISiMeasure<TUnit>, ILinear
    where TUnit : ISiDerivedUnit
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
}
internal readonly struct SiDerived<TPrefix, TUnit> : ISiMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiDerivedUnit
{
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}
internal readonly struct Other<TUnit> : IOtherMeasure<TUnit>, ILinear
    where TUnit : IUnit, ITransform, IRepresentable
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
}
internal readonly struct SiAccepted<TUnit> : ISiAccepted<TUnit>, ILinear
    where TUnit : ISiAcceptedUnit
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
}
internal readonly struct SiAccepted<TPrefix, TUnit> : ISiAccepted<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiAcceptedUnit
{
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}
internal readonly struct Divide<TNominator, TDenominator> : IMeasure
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public static Double ToSi(in Double value) => TDenominator.FromSi(TNominator.ToSi(in value));
    public static Double FromSi(in Double value) => TDenominator.ToSi(TNominator.FromSi(in value));
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
}

internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IDimension
    where TMeasure : IMeasure
{
    private static readonly Double toSi = Pow(TMeasure.ToSi(1d), TDim.Exponent);
    private static readonly Double fromSi = Pow(TMeasure.FromSi(1d), TDim.Exponent);
    public static Double ToSi(in Double value) => toSi * value;
    public static Double FromSi(in Double value) => fromSi * value;
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
}
