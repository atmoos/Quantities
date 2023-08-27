using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

using static Quantities.Extensions;

namespace Quantities.Measures;

internal readonly struct Si<TUnit> : ISiMeasure<TUnit>
    where TUnit : ISiUnit, Dimensions.IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static IOperations Operations { get; } = FromScalar<Si<TUnit>>.Create<TUnit>();
    public static Boolean Is<TDimension>() where TDimension : Dimensions.IDimension => TUnit.Is<TDimension>();
    public static (Double, T) Lower<T>(IInject<T> inject, in Double value) => (value, inject.Inject<Si<TUnit>>(in value));
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        return scaling.Scale(new PrefixSi<T, TUnit>(inject), in value);
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
        return scaling.Scale(new PrefixSi<T, TUnit>(inject), poly.Evaluate(value));
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
        return scaling.Scale(new PrefixMetric<T, TUnit>(inject), in value);
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
        return scaling.Scale(new PrefixMetric<T, TUnit>(inject), poly.Evaluate(value));
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
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        var (rightScaled, injector) = TRight.Lower(new ProductInject<T>(inject), value);
        return TLeft.Normalize(scaling, injector, rightScaled);
    }

    public static (Double, T) Lower<T>(IInject<T> inject, in Double value)
    {
        var (tempValue, rightInject) = TRight.Lower(new ProductInject<T>(inject), in value);
        return TLeft.Lower(rightInject, tempValue);
    }
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
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        var (scaledDenominator, nominatorInjector) = TDenominator.Lower(new QuotientInject<T>(inject), 1d);
        return TNominator.Normalize(scaling, nominatorInjector, value / scaledDenominator);
    }

    public static (Double, T) Lower<T>(IInject<T> inject, in Double value)
    {
        var (scaledDenominator, nominatorInjector) = TDenominator.Lower(new QuotientInject<T>(inject), 1d);
        return TNominator.Lower(nominatorInjector, value / scaledDenominator);
    }
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
    public static T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value)
    {
        var (_, result) = TMeasure.Lower(new PowerNormalizing<T>(scaling, inject, value), 1d);
        return result;
    }

    public static (Double, T) Lower<T>(IInject<T> inject, in Double value)
    {
        var (_, (poly, result)) = TMeasure.Lower(new PowerLowering<T>(inject), in value);
        return (poly.Evaluate(in value), result);
    }

    private sealed class PowerLowering<T> : IInject<(Polynomial, T)>
    {
        private readonly IInject<T> injector;
        public PowerLowering(IInject<T> injector) => this.injector = injector;
        public (Polynomial, T) Inject<TMeasure1>(in Double value) where TMeasure1 : IMeasure
        {
            return (Conversion<Power<TDim, TMeasure>, Power<TDim, TMeasure1>>.Polynomial, this.injector.Inject<Power<TDim, TMeasure1>>(in value));
        }
    }
    private class Normalizer<TLowered, T> : IInject<T>
        where TLowered : IMeasure
    {
        private readonly IInject<T> injector;
        public Normalizer(IInject<T> injector) => this.injector = injector;
        public T Inject<TMeasure1>(in Double value) where TMeasure1 : IMeasure
        {
            return this.injector.Inject<Power<TDim, TMeasure1>>(TDim.Power(value));
        }
    }
    private sealed class PowerNormalizing<T> : IInject<T>
    {
        private readonly Double value;
        private readonly IInject<T> injector;
        private readonly IPrefixScale scaling;
        public PowerNormalizing(IPrefixScale scaling, IInject<T> injector, Double value)
        {
            this.injector = injector;
            this.scaling = scaling;
            this.value = value;
        }

        public T Inject<TMeasure1>(in Double value) where TMeasure1 : IMeasure
        {
            var poly = Conversion<Power<TDim, TMeasure>, Power<TDim, TMeasure1>>.Polynomial;
            return TMeasure1.Normalize(this.scaling, new Normalizer<TMeasure1, T>(this.injector), TDim.Lower(poly.Evaluate(this.value)));
        }
    }
}

file sealed class PrefixSi<T, TUnit> : IPrefixInject<T>
    where TUnit : ISiUnit, Dimensions.IDimension
{
    private readonly IInject<T> injector;
    public PrefixSi(IInject<T> injector) => this.injector = injector;
    public T Identity(in Double value) => this.injector.Inject<Si<TUnit>>(in value);
    public T Inject<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return this.injector.Inject<Si<TPrefix, TUnit>>(in value);
    }
}

file sealed class PrefixMetric<T, TUnit> : IPrefixInject<T>
    where TUnit : IMetricUnit, Dimensions.IDimension
{
    private readonly IInject<T> injector;
    public PrefixMetric(IInject<T> injector) => this.injector = injector;
    public T Identity(in Double value) => this.injector.Inject<Metric<TUnit>>(in value);
    public T Inject<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return this.injector.Inject<Metric<TPrefix, TUnit>>(in value);
    }
}

file sealed class PrefixProduct<T, TUnit> : IPrefixInject<T>
    where TUnit : IMetricUnit, Dimensions.IDimension
{
    private readonly IInject<T> injector;
    public PrefixProduct(IInject<T> injector) => this.injector = injector;
    public T Identity(in Double value) => this.injector.Inject<Metric<TUnit>>(in value);
    public T Inject<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return this.injector.Inject<Metric<TPrefix, TUnit>>(in value);
    }
}

file sealed class ProductInject<T> : IInject<IInject<T>>
{
    private readonly IInject<T> injector;
    public ProductInject(IInject<T> injector) => this.injector = injector;
    public IInject<T> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new Right<TMeasure>(this.injector);

    private sealed class Right<TRight> : IInject<T>
        where TRight : IMeasure
    {
        private readonly IInject<T> injector;
        public Right(IInject<T> injector) => this.injector = injector;
        public T Inject<TMeasure>(in Double value) where TMeasure : IMeasure => this.injector.Inject<Product<TMeasure, TRight>>(in value);
    }
}

file sealed class QuotientInject<T> : IInject<IInject<T>>
{
    private readonly IInject<T> injector;
    public QuotientInject(IInject<T> injector) => this.injector = injector;
    public IInject<T> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => new Left<TMeasure>(this.injector);

    private sealed class Left<TDenominator> : IInject<T>
        where TDenominator : IMeasure
    {
        private readonly IInject<T> injector;
        public Left(IInject<T> injector) => this.injector = injector;
        public T Inject<TMeasure>(in Double value) where TMeasure : IMeasure => this.injector.Inject<Quotient<TMeasure, TDenominator>>(in value);
    }
}
