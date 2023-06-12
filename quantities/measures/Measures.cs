using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Measures;

internal readonly struct Si<TUnit> : ISiMeasure<TUnit>, ISerialize, ILinear
    where TUnit : ISiUnit
{
    private static readonly Serializer<TUnit> serializer = new("si");
    public static Double ToSi(in Double value) => value;
    public static Double FromSi(in Double value) => value;
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : ISiMeasure<TUnit>, ISerialize, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new("si");
    public static Double ToSi(in Double value) => TPrefix.ToSi(in value);
    public static Double FromSi(in Double value) => TPrefix.FromSi(in value);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMetricMeasure<TUnit>, ISerialize, ILinear
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit> serializer = new("metric");
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMetricMeasure<TUnit>, ISerialize, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new("metric");
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IImperialMeasure<TUnit>, ISerialize, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new("imperial");
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : INonStandardMeasure<TUnit>, ISerialize, ILinear
    where TUnit : INoSystemUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IMeasure, ISerialize
    where TLeft : IMeasure, ISerialize
    where TRight : IMeasure, ISerialize
{
    const String narrowNoBreakSpace = "\u202F";
    public static Double ToSi(in Double value) => TLeft.ToSi(TRight.ToSi(in value));
    public static Double FromSi(in Double value) => TRight.FromSi(TLeft.FromSi(in value));
    public static String Representation { get; } = $"{TLeft.Representation}{narrowNoBreakSpace}{TRight.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("prod");
        TLeft.Write(writer);
        TRight.Write(writer);
        writer.End();
    }
}
internal readonly struct Fraction<TNominator, TDenominator> : IMeasure, ISerialize
    where TNominator : IMeasure, ISerialize
    where TDenominator : IMeasure, ISerialize
{
    public static Double ToSi(in Double value) => TDenominator.FromSi(TNominator.ToSi(in value));
    public static Double FromSi(in Double value) => TNominator.FromSi(TDenominator.ToSi(in value));
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("frac");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
}
internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IDimension
    where TMeasure : IMeasure
{
    private static readonly Double toSi = TDim.Pow(TMeasure.ToSi(1d));
    private static readonly Double fromSi = TDim.Pow(TMeasure.FromSi(1d));
    public static Double ToSi(in Double value) => toSi * value;
    public static Double FromSi(in Double value) => fromSi * value;
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start(typeof(TDim).Name);
        TMeasure.Write(writer);
        writer.End();
    }
}
