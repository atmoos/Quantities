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
    public static IOperations Operations { get; } = new FromScalar<Si<TUnit>>();
    public static Transformation ToSi(Transformation self) => self;
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : ISiMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Si<TUnit>));
    public static IOperations Operations { get; } = new FromScalar<Si<TPrefix, TUnit>>();
    public static Transformation ToSi(Transformation self) => TPrefix.ToSi(self);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMetricMeasure<TUnit>, ILinear
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static IOperations Operations { get; } = new FromScalar<Metric<TUnit>>();
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMetricMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static IOperations Operations { get; } = new FromScalar<Metric<TPrefix, TUnit>>();
    public static Transformation ToSi(Transformation self) => TPrefix.ToSi(TUnit.ToSi(self));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IImperialMeasure<TUnit>, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static IOperations Operations { get; } = new FromScalar<Imperial<TUnit>>();
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : INonStandardMeasure<TUnit>, ILinear
    where TUnit : INoSystemUnit, ITransform, IRepresentable
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static IOperations Operations { get; } = new FromScalar<NonStandard<TUnit>>();
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    const String narrowNoBreakSpace = "\u202F";
    public static IOperations Operations { get; } = new FromProduct<TLeft, TRight>();
    public static Transformation ToSi(Transformation self) => TLeft.ToSi(TRight.ToSi(self));
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
    public static IOperations Operations { get; } = new FromQuotient<TNominator, TDenominator>();
    public static Transformation ToSi(Transformation self) => TNominator.ToSi(TDenominator.ToSi(self).Invert());
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
    public static IOperations Operations { get; } = new FromPower<TDim, TMeasure>();
    public static Transformation ToSi(Transformation self) => TDim.Pow(TMeasure.ToSi(self));
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TMeasure.Write(writer);
        writer.End();
    }
}
