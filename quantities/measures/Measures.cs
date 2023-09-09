using Quantities.Dimensions;
using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

using static Quantities.Extensions;
using static Quantities.Dimensions.Rank;

namespace Quantities.Measures;

internal readonly struct Si<TUnit> : IMeasure, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.NoOp;
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<Si<TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : IMeasure, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix>();
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<Si<TPrefix, TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMeasure, ILinear
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<Metric<TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMeasure, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit, TPrefix> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix, TUnit>();
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<Metric<TPrefix, TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IMeasure, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<Imperial<TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : IMeasure, ILinear
    where TUnit : INoSystemUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarProduct<NonStandard<TUnit>, TMeasure>.Result;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => TMeasure.RankOf<TUnit>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TUnit.RankOf<TDimension>();
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    private const String zeroWidthNonJoiner = "\u200C"; // https://en.wikipedia.org/wiki/Zero-width_non-joiner
    public static Polynomial Poly { get; } = TLeft.Poly * TRight.Poly;
    public static String Representation { get; } = $"{TLeft.Representation}{zeroWidthNonJoiner}{TRight.Representation}";
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => throw NotImplemented<Product<TLeft, TRight>>();
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => throw NotImplemented<Product<TLeft, TRight>>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => throw NotImplemented<Product<TLeft, TRight>>();
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
    public static Polynomial Poly { get; } = TNominator.Poly / TDenominator.Poly;
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => throw NotImplemented<Quotient<TNominator, TDenominator>>();
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => throw NotImplemented<Quotient<TNominator, TDenominator>>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => throw NotImplemented<Quotient<TNominator, TDenominator>>();
    public static void Write(IWriter writer)
    {
        writer.Start("quotient");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
}
internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IExponent
    where TMeasure : IMeasure
{
    private static readonly String dimension = typeof(TDim).Name.ToLowerInvariant();
    public static Polynomial Poly { get; } = TDim.Pow(TMeasure.Poly);
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
    public static Result Multiply<TOtherMeasure>() where TOtherMeasure : IMeasure => throw NotImplemented<Power<TDim, TMeasure>>();
    public static Rank Rank<TOtherMeasure>()
         where TOtherMeasure : IMeasure => Map(TMeasure.Rank<TOtherMeasure>());
    public static Rank RankOf<TDimension>() where TDimension : IDimension => Map(TMeasure.RankOf<TDimension>());
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TMeasure.Write(writer);
        writer.End();
    }
    private static Rank Map(in Rank rank) => rank switch {
        // ToDo: Other ranks could be mapped via "offsetting" (or & and operators)
        Linear => TDim.Rank,
        _ => None
    };
}

file static class ScalarProduct<TScalar, TRight>
    where TScalar : IMeasure, ILinear
    where TRight : IMeasure
{
    public static Result Result { get; } = Compute.ScalarProduct<TScalar, TRight>();
}

file static class Compute
{
    public static Result ScalarProduct<TLeft, TRight>()
        where TLeft : IMeasure, ILinear
        where TRight : IMeasure
    {
        return TLeft.Rank<TRight>() switch {
            Linear => new Result(TLeft.Poly / TRight.Poly, Measure.Of<Power<Square, TLeft>, Linear<TLeft>>()),
            Rank.Square => new Result(TLeft.Poly / TRight.Poly, Measure.Of<Power<Cubic, TLeft>, Linear<TLeft>>()),
            _ => new Result(Polynomial.NoOp, Measure.Of<Product<TLeft, TRight>>())
        };
    }
}
