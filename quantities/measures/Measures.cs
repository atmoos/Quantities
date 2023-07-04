using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Measures;

internal readonly struct Si<TUnit> : ISiMeasure<TUnit>, ILinear
    where TUnit : ISiUnit
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Double ToSi(in Double value) => value;
    public static Double FromSi(in Double value) => value;
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : ISiMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Si<TUnit>));
    public static Double ToSi(in Double value) => TPrefix.ToSi(in value);
    public static Double FromSi(in Double value) => TPrefix.FromSi(in value);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMetricMeasure<TUnit>, ILinear
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMetricMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IImperialMeasure<TUnit>, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : INonStandardMeasure<TUnit>, ILinear
    where TUnit : INoSystemUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    const String narrowNoBreakSpace = "\u202F";
    public static Double ToSi(in Double value) => TLeft.ToSi(TRight.ToSi(in value));
    public static Double FromSi(in Double value) => TRight.FromSi(TLeft.FromSi(in value));
    public static String Representation { get; } = $"{TLeft.Representation}{narrowNoBreakSpace}{TRight.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("product");
        TLeft.Write(writer);
        TRight.Write(writer);
        writer.End();
    }
}
internal readonly struct Quotient<TNominator, TDenominator> : IMeasure
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public static Double ToSi(in Double value) => TDenominator.FromSi(TNominator.ToSi(in value));
    public static Double FromSi(in Double value) => TNominator.FromSi(TDenominator.ToSi(in value));
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("quotient");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
}
internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IDimension
    where TMeasure : IMeasure
{
    private static readonly String dimension = typeof(TDim).Name.ToLowerInvariant();
    private static readonly Double toSi = TDim.Pow(TMeasure.ToSi(1d));
    private static readonly Double fromSi = TDim.Pow(TMeasure.FromSi(1d));
    public static Double ToSi(in Double value) => toSi * value;
    public static Double FromSi(in Double value) => fromSi * value;
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TMeasure.Write(writer);
        writer.End();
    }
}
