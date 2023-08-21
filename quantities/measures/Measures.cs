using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

using static Quantities.Extensions;

namespace Quantities.Measures;

file sealed class SiPrefix<T, TUnit> : IPrefixInject<T>
    where TUnit : ISiUnit, Dimensions.IDimension
{
    private readonly IInject<T> injector;
    public SiPrefix(IInject<T> injector) => this.injector = injector;
    public T Identity(in Double value) => this.injector.Inject<Si<TUnit>>(in value);
    public T Inject<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return this.injector.Inject<Si<TPrefix, TUnit>>(in value);
    }
}

file sealed class MetricPrefix<T, TUnit> : IPrefixInject<T>
    where TUnit : IMetricUnit, Dimensions.IDimension
{
    private readonly IInject<T> injector;
    public MetricPrefix(IInject<T> injector) => this.injector = injector;
    public T Identity(in Double value) => this.injector.Inject<Metric<TUnit>>(in value);
    public T Inject<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return this.injector.Inject<Metric<TPrefix, TUnit>>(in value);
    }
}

internal readonly struct Si<TUnit> : ISiMeasure<TUnit>
    where TUnit : ISiUnit, Dimensions.IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static IOperations Operations { get; } = FromScalar<Si<TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => (value, inject.Inject<Si<TUnit>>(in value));
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        return scaling.Scale(new SiPrefix<T, TUnit>(inject), in value);
    }
    public static Transformation ToSi(Transformation self) => self;
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : ISiMeasure<TUnit>
    where TPrefix : IPrefix
    where TUnit : ISiUnit, Dimensions.IDimension
{
    private static readonly Polynomial poly = Polynomial.Of<TPrefix>();
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Si<TUnit>));
    public static IOperations Operations { get; } = FromScalar<Si<TPrefix, TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value)
    {
        var lowered = poly.Evaluate(in value);
        return (lowered, inject.Inject<Si<TUnit>>(in lowered));
    }
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        return scaling.Scale(new SiPrefix<T, TUnit>(inject), poly.Evaluate(value));
    }
    public static Transformation ToSi(Transformation self) => TPrefix.ToSi(self);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMetricMeasure<TUnit>
    where TUnit : IMetricUnit, Dimensions.IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static IOperations Operations { get; } = FromScalar<Metric<TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => (value, inject.Inject<Metric<TUnit>>(in value));
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        return scaling.Scale(new MetricPrefix<T, TUnit>(inject), in value);
    }
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMetricMeasure<TUnit>
    where TPrefix : IPrefix
    where TUnit : IMetricUnit, Dimensions.IDimension
{
    private static readonly Polynomial poly = Polynomial.Of<TPrefix>();
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static IOperations Operations { get; } = FromScalar<Metric<TPrefix, TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value)
    {
        var lowered = poly.Evaluate(in value);
        return (lowered, inject.Inject<Metric<TUnit>>(in lowered));
    }
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        return scaling.Scale(new MetricPrefix<T, TUnit>(inject), poly.Evaluate(value));
    }
    public static Transformation ToSi(Transformation self) => TPrefix.ToSi(TUnit.ToSi(self));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IImperialMeasure<TUnit>
    where TUnit : IImperialUnit, ITransform, IRepresentable, Dimensions.IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static IOperations Operations { get; } = FromScalar<Imperial<TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value) => inject.Inject<Imperial<TUnit>>(in value);
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => (value, inject.Inject<Imperial<TUnit>>(in value));
}
internal readonly struct NonStandard<TUnit> : INonStandardMeasure<TUnit>
    where TUnit : INoSystemUnit, ITransform, IRepresentable, Dimensions.IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static IOperations Operations { get; } = FromScalar<NonStandard<TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static Transformation ToSi(Transformation self) => TUnit.ToSi(self);
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer) => serializer.Write(writer);
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value) => inject.Inject<NonStandard<TUnit>>(in value);
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => (value, inject.Inject<NonStandard<TUnit>>(in value));
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    const String narrowNoBreakSpace = "\u202F";
    public static IOperations Operations { get; } = new FromProduct<TLeft, TRight>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => false; // ToDo!
    public static Transformation ToSi(Transformation self) => TLeft.ToSi(TRight.ToSi(self));
    public static String Representation { get; } = $"{TLeft.Representation}{narrowNoBreakSpace}{TRight.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("product");
        TLeft.Write(writer);
        TRight.Write(writer);
        writer.End();
    }
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value) => throw NotImplemented<Product<TLeft, TRight>>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => throw NotImplemented<Product<TLeft, TRight>>();
}
internal readonly struct Quotient<TNominator, TDenominator> : IMeasure
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public static IOperations Operations { get; } = new FromQuotient<TNominator, TDenominator>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => false; // ToDo!
    public static Transformation ToSi(Transformation self) => TNominator.ToSi(TDenominator.ToSi(self).Invert());
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start("quotient");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value) => throw NotImplemented<Quotient<TNominator, TDenominator>>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => throw NotImplemented<Quotient<TNominator, TDenominator>>();
}
internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IDimension
    where TMeasure : IMeasure
{
    private static readonly String dimension = typeof(TDim).Name.ToLowerInvariant();
    public static IOperations Operations { get; } = new FromPower<TDim, TMeasure>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => false; // ToDo!
    public static Transformation ToSi(Transformation self) => TDim.Pow(TMeasure.ToSi(self));
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TMeasure.Write(writer);
        writer.End();
    }
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value) => throw NotImplemented<Power<TDim, TMeasure>>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => throw NotImplemented<Power<TDim, TMeasure>>();
}
