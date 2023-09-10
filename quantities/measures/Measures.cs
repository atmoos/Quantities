using Quantities.Dimensions;
using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

using static Quantities.Dimensions.Rank;

namespace Quantities.Measures;

file interface IOps
{
    static abstract Result Product { get; }
    static abstract Result Quotient { get; }
}

file interface ICompute
{
    static abstract Result Times<TMeasure>() where TMeasure : IMeasure;
    static abstract Result Per<TMeasure>() where TMeasure : IMeasure;
}

internal readonly struct Identity : IMeasure, ILinear
{
    private static readonly String name = nameof(Identity).ToLowerInvariant();
    public static Polynomial Poly => Polynomial.One;
    public static String Representation => "𝟙";
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => new(Polynomial.One / TMeasure.Poly, Measure.Of<Quotient<Identity, TMeasure>>());
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => new(Polynomial.One, Measure.Of<TMeasure>());
    public static Rank Rank<TMeasure>() where TMeasure : IMeasure => None; // Or Zero?
    public static Rank RankOf<TDimension>() where TDimension : IDimension => None; // Or Zero?
    public static void Write(IWriter writer) => writer.Write(name, Representation);
}

internal readonly struct Si<TUnit> : IMeasure, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.One;
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Si<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Si<TUnit>, TMeasure>.Quotient;
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
        where TMeasure : IMeasure => ScalarOps<Si<TPrefix, TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Si<TPrefix, TUnit>, TMeasure>.Quotient;
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
        where TMeasure : IMeasure => ScalarOps<Metric<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Metric<TUnit>, TMeasure>.Quotient;
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
        where TMeasure : IMeasure => ScalarOps<Metric<TPrefix, TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Metric<TPrefix, TUnit>, TMeasure>.Quotient;
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
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Quotient;
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
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Quotient;
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
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Quotient;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => Compute.HigherOrder(TLeft.Rank<TMeasure>(), TRight.Rank<TMeasure>());
    public static Rank RankOf<TDimension>()
        where TDimension : IDimension => Compute.HigherOrder(TLeft.RankOf<TDimension>(), TRight.RankOf<TDimension>());
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
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Quotient;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => Compute.HigherOrder(TNominator.Rank<TMeasure>(), TDenominator.Rank<TMeasure>());
    public static Rank RankOf<TDimension>()
        where TDimension : IDimension => Compute.HigherOrder(TNominator.RankOf<TDimension>(), TDenominator.RankOf<TDimension>());
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
    public static Result Multiply<TOtherMeasure>() where TOtherMeasure : IMeasure => PowerOps<TDim, TMeasure, TOtherMeasure>.Product;
    public static Result Divide<TOtherMeasure>() where TOtherMeasure : IMeasure => PowerOps<TDim, TMeasure, TOtherMeasure>.Quotient;
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


file sealed class ScalarOps<TScalar, TArgument> : IOps
    where TScalar : IMeasure, ILinear
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Scalar<TScalar>.Times<TArgument>();
    public static Result Quotient { get; } = Compute.Scalar<TScalar>.Per<TArgument>();
}

file sealed class ProductOps<TLeft, TRight, TArgument> : IOps
    where TLeft : IMeasure
    where TRight : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Prod<TLeft, TRight>.Times<TArgument>();
    public static Result Quotient { get; } = Compute.Prod<TLeft, TRight>.Per<TArgument>();
}

file sealed class QuotientOps<TNominator, TDenominator, TArgument> : IOps
    where TNominator : IMeasure
    where TDenominator : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Div<TNominator, TDenominator>.Times<TArgument>();
    public static Result Quotient { get; } = Compute.Div<TNominator, TDenominator>.Per<TArgument>();
}

file sealed class PowerOps<TDim, TLinear, TArgument> : IOps
    where TDim : IExponent
    where TLinear : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Pow<TDim, TLinear>.Times<TArgument>();
    public static Result Quotient { get; } = Compute.Pow<TDim, TLinear>.Per<TArgument>();
}

// Everything in here is allowed to be comparatively inefficient, as it is
// only every executed once during the lifetime of any dependent app.
file static class Compute
{
    public static Rank HigherOrder(in Rank nominator, in Rank denominator) => nominator is Linear || denominator is Linear ? Rank.HigherOrder : None;

    public sealed class Scalar<TLeft> : ICompute
        where TLeft : IMeasure, ILinear
    {
        public static Result Times<TArgument>() where TArgument : IMeasure
        {
            return TLeft.Rank<TArgument>() switch {
                Linear => new Result(TArgument.Poly / TLeft.Poly, Measure.Of<Power<Square, TLeft>, Linear<TLeft>>()),
                Rank.Square => new Result(TArgument.Poly / TLeft.Poly, Measure.Of<Power<Cubic, TLeft>, Linear<TLeft>>()),
                Rank.HigherOrder => TArgument.Multiply<TLeft>(),
                _ => new Result(Polynomial.One, Measure.Of<Product<TLeft, TArgument>>())
            };
        }
        public static Result Per<TArgument>() where TArgument : IMeasure
        {
            return TLeft.Rank<TArgument>() switch {
                Linear => new Result(TArgument.Poly / TLeft.Poly, Measure.Of<Identity>()),
                Rank.Square => new Result(TArgument.Poly / TLeft.Poly, Measure.Of<TLeft>()),
                Rank.Cubic => new Result(TArgument.Poly / TLeft.Poly, Measure.Of<Power<Square, TLeft>, Linear<TLeft>>()),
                Rank.HigherOrder => TArgument.Divide<TLeft>(),
                _ => new Result(Polynomial.One, Measure.Of<Quotient<TLeft, TArgument>>())
            };
        }
    }

    public sealed class Prod<TLeft, TRight> : ICompute
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Times<TArgument>() where TArgument : IMeasure
        {
            if (TLeft.Rank<TArgument>() == Linear) {
                return new(TLeft.Poly / TArgument.Poly, Measure.Of<Product<Power<Square, TLeft>, TRight>>());
            }
            if (TRight.Rank<TArgument>() == Linear) {
                return new(TRight.Poly / TArgument.Poly, Measure.Of<Product<TLeft, Power<Square, TRight>>>());
            }
            return new(Polynomial.One, Measure.Of<Product<Product<TLeft, TRight>, TArgument>>());
        }
        public static Result Per<TArgument>() where TArgument : IMeasure
        {
            if (TLeft.Rank<TArgument>() == Linear) {
                return new(TLeft.Poly / TArgument.Poly, Measure.Of<TRight>());
            }
            if (TRight.Rank<TArgument>() == Linear) {
                return new(TRight.Poly / TArgument.Poly, Measure.Of<TLeft>());
            }
            return new(Polynomial.One, Measure.Of<Quotient<Product<TLeft, TRight>, TArgument>>());
        }
    }

    public sealed class Div<TNominator, TDenominator> : ICompute
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        public static Result Times<TArgument>() where TArgument : IMeasure
        {
            if (TNominator.Rank<TArgument>() == Linear) {
                return new(TNominator.Poly / TArgument.Poly, Measure.Of<Quotient<Power<Square, TNominator>, TDenominator>>());
            }
            if (TDenominator.Rank<TArgument>() == Linear) {
                return new(TArgument.Poly / TDenominator.Poly, Measure.Of<TNominator>());
            }
            return new(Polynomial.One, Measure.Of<Product<Quotient<TNominator, TDenominator>, TArgument>>());
        }
        public static Result Per<TArgument>() where TArgument : IMeasure
        {
            if (TNominator.Rank<TArgument>() == Linear) {
                return new(TNominator.Poly / TArgument.Poly, Measure.Of<Quotient<Identity, TDenominator>>());
            }
            if (TDenominator.Rank<TArgument>() == Linear) {
                return new(TArgument.Poly / TDenominator.Poly, Measure.Of<Quotient<TNominator, Power<Square, TDenominator>>>());
            }
            return new(Polynomial.One, Measure.Of<Quotient<TNominator, Product<TDenominator, TArgument>>>());
        }
    }
    public sealed class Pow<TExp, TLinear> : ICompute
        where TExp : IExponent
        where TLinear : IMeasure
    {
        private static readonly Rank dimension = TExp.Rank;
        public static Result Times<TArgument>() where TArgument : IMeasure
        {
            if (TLinear.Rank<TArgument>() == Linear) {
                var conversion = TArgument.Poly / TLinear.Poly;
                if (dimension == Linear) {
                    return new(in conversion, Measure.Of<Power<Square, TLinear>, Linear<TLinear>>());
                }
                if (dimension == Rank.Square) {
                    return new(in conversion, Measure.Of<Power<Cubic, TLinear>, Linear<TLinear>>());
                }
            }
            return new(Polynomial.One, Measure.Of<Product<Power<TExp, TLinear>, TArgument>>());
        }

        public static Result Per<TArgument>() where TArgument : IMeasure
        {
            if (TLinear.Rank<TArgument>() == Linear) {
                var conversion = TLinear.Poly / TArgument.Poly;
                if (dimension == Linear) {
                    return new(in conversion, Measure.Of<Identity>());
                }
                if (dimension == Rank.Square) {
                    return new(in conversion, Measure.Of<TLinear>());
                }
                if (dimension == Rank.Cubic) {
                    return new(in conversion, Measure.Of<Power<Square, TLinear>, Linear<TLinear>>());
                }
            }
            return new(Polynomial.One, Measure.Of<Quotient<Power<TExp, TLinear>, TArgument>>());
        }
    }
}
