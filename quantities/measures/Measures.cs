using Quantities.Dimensions;
using Quantities.Numerics;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

using static Quantities.Dimensions.Rank;
using static Quantities.Measures.Convenience;

namespace Quantities.Measures;

file interface IOps
{
    static abstract Result Product { get; }
    static abstract Result Quotient { get; }
}

file interface ICompute
{
    static abstract Result Multiply<TMeasure>() where TMeasure : IMeasure;
    static abstract Result Divide<TMeasure>() where TMeasure : IMeasure;
}

// ToDo: what should 1/M be? M⁻¹, or simply 1/M?

internal readonly struct Identity : IMeasure, ILinear
{
    private static readonly String name = nameof(Identity).ToLowerInvariant();
    public static Polynomial Poly => Polynomial.One;
    public static String Representation => "𝟙";
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => new(Polynomial.One / TMeasure.Poly, Measure.Of<Quotient<Identity, TMeasure>>());
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => new(Polynomial.One, Measure.Of<TMeasure>());
    public static Rank Rank<TMeasure>() where TMeasure : IMeasure => Zero;
    public static Rank RankOf<TDimension>() where TDimension : IDimension => Zero;
    public static void Write(IWriter writer) => writer.Write(name, Representation);
}

internal readonly struct Si<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.One;
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Si<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Si<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Si<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
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
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Metric<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Metric<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Metric<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
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
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct Imperial<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : INoSystemUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IProduct<TLeft, TRight>, IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    private const String zeroWidthNonJoiner = "\u200C"; // https://en.wikipedia.org/wiki/Zero-width_non-joiner
    public static Polynomial Poly { get; } = TLeft.Poly * TRight.Poly;
    public static String Representation { get; } = $"{TLeft.Representation}{zeroWidthNonJoiner}{TRight.Representation}";
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Quotient;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => Compute.OrRanks(TLeft.Rank<TMeasure>(), TRight.Rank<TMeasure>());
    public static Rank RankOf<TDimension>()
        where TDimension : IDimension => Compute.OrRanks(TLeft.RankOf<TDimension>(), TRight.RankOf<TDimension>());
    public static void Write(IWriter writer)
    {
        writer.Start("product");
        TLeft.Write(writer);
        TRight.Write(writer);
        writer.End();
    }
}
internal readonly struct Quotient<TNominator, TDenominator> : IQuotient<TNominator, TDenominator>, IMeasure
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public static Polynomial Poly { get; } = TNominator.Poly / TDenominator.Poly;
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Quotient;
    public static Rank Rank<TMeasure>()
         where TMeasure : IMeasure => Compute.OrRanks(TNominator.Rank<TMeasure>(), TDenominator.Rank<TMeasure>());
    public static Rank RankOf<TDimension>()
        where TDimension : IDimension => Compute.OrRanks(TNominator.RankOf<TDimension>(), TDenominator.RankOf<TDimension>());
    public static void Write(IWriter writer)
    {
        writer.Start("quotient");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
}

// ToDo: Make use of all the rank info here!!
internal readonly struct Alias<TAlias, TLinear> : IMeasure
    where TAlias : IMeasure
    where TLinear : IMeasure, ILinear
{
    public static Polynomial Poly => TAlias.Poly;
    public static String Representation => TAlias.Representation;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => HigherOrderOps<TAlias, TLinear, TMeasure>.Quotient;
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => HigherOrderOps<TAlias, TLinear, TMeasure>.Product;
    public static Rank Rank<TMeasure>() where TMeasure : IMeasure => TAlias.Rank<TMeasure>();
    public static Rank RankOf<TDimension>() where TDimension : IDimension => TAlias.RankOf<TDimension>();
    public static void Write(IWriter writer) => TAlias.Write(writer);
}

internal readonly struct Power<TDim, TLinear> : IMeasure
    where TDim : IExponent
    where TLinear : IMeasure
{
    private static readonly String dimension = typeof(TDim).Name.ToLowerInvariant();
    public static Polynomial Poly { get; } = TDim.Pow(TLinear.Poly);
    public static String Representation { get; } = $"{TLinear.Representation}{TDim.Representation}";
    public static Result Multiply<TOtherMeasure>() where TOtherMeasure : IMeasure => HigherOrderOps<Power<TDim, TLinear>, TLinear, TOtherMeasure>.Product;
    public static Result Divide<TOtherMeasure>() where TOtherMeasure : IMeasure => HigherOrderOps<Power<TDim, TLinear>, TLinear, TOtherMeasure>.Quotient;
    public static Rank Rank<TOtherMeasure>()
         where TOtherMeasure : IMeasure => Map(TLinear.Rank<TOtherMeasure>());
    public static Rank RankOf<TDimension>() where TDimension : IDimension => Map(TLinear.RankOf<TDimension>());
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TLinear.Write(writer);
        writer.End();
    }
    private static Rank Map(in Rank rank) => rank switch {
        One => TDim.Rank,
        var self when self == TDim.Rank => One,
        _ => rank
    };
}


file sealed class ScalarOps<TScalar, TArgument> : IOps
    where TScalar : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Scalar<TScalar>.Multiply<TArgument>();
    public static Result Quotient { get; } = Compute.Scalar<TScalar>.Divide<TArgument>();
}

file sealed class ProductOps<TLeft, TRight, TArgument> : IOps
    where TLeft : IMeasure
    where TRight : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Prod<TLeft, TRight>.Multiply<TArgument>();
    public static Result Quotient { get; } = Compute.Prod<TLeft, TRight>.Divide<TArgument>();
}

file sealed class QuotientOps<TNominator, TDenominator, TArgument> : IOps
    where TNominator : IMeasure
    where TDenominator : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.Div<TNominator, TDenominator>.Multiply<TArgument>();
    public static Result Quotient { get; } = Compute.Div<TNominator, TDenominator>.Divide<TArgument>();
}

file sealed class HigherOrderOps<THigher, TLinear, TArgument> : IOps
    where THigher : IMeasure
    where TLinear : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Compute.HighOrder<THigher, TLinear>.Multiply<TArgument>();
    public static Result Quotient { get; } = Compute.HighOrder<THigher, TLinear>.Divide<TArgument>();
}

// Everything in here is allowed to be comparatively inefficient, as it is
// only ever executed once during the lifetime of any dependent app.
file static class Compute
{
    public static Rank OrRanks(in Rank left, in Rank right) => left is One || right is One ? HigherOrder : None;

    public sealed class Scalar<TSelf> : ICompute
        where TSelf : IMeasure
    {
        public static Result Multiply<TArgument>()
            where TArgument : IMeasure => TSelf.Rank<TArgument>() switch {
                Zero => new Result(Polynomial.One, Measure.Of<TSelf>()),
                One => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<Power<Square, TSelf>>()),
                Two => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<Power<Cubic, TSelf>>()),
                HigherOrder => TArgument.Multiply<TSelf>(),
                _ => new Result(Polynomial.One, Measure.Of<Product<TSelf, TArgument>>())
            };

        public static Result Divide<TArgument>()
            where TArgument : IMeasure => TSelf.Rank<TArgument>() switch {
                Zero => new Result(Polynomial.One, Measure.Of<TSelf>()),
                One => new Result(TSelf.Poly / TArgument.Poly, Measure.Of<Identity>()),
                Two => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<TSelf>()),
                Three => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<Power<Square, TSelf>>()),
                // HigherOrder => TArgument.RightDivide<TLeft>(), ToDo...
                _ => new Result(Polynomial.One, Measure.Of<Quotient<TSelf, TArgument>>())
            };
    }

    public sealed class Prod<TLeft, TRight> : ICompute
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        public static Result Multiply<TArgument>() where TArgument : IMeasure
        {
            var left = TLeft.Rank<TArgument>();
            var right = TRight.Rank<TArgument>();
            var (poly, measure) = (left, right) switch {
                (One, One) => (TArgument.Poly * TRight.Poly / TLeft.Poly, Measure.Of<Power<Cubic, TLeft>>()),
                (One, _) => (TArgument.Poly / TLeft.Poly, Measure.Of<Product<Power<Square, TLeft>, TRight>>()),
                (_, One) => (TArgument.Poly / TRight.Poly, Measure.Of<Product<TLeft, Power<Square, TRight>>>()),
                _ => (Polynomial.One, Measure.Of<Product<Product<TLeft, TRight>, TArgument>>())
            };
            return new(in poly, measure);
        }
        public static Result Divide<TArgument>() where TArgument : IMeasure
        {
            if (TLeft.Rank<TArgument>() == One) {
                return new(TLeft.Poly / TArgument.Poly, Measure.Of<TRight>());
            }
            if (TRight.Rank<TArgument>() == One) {
                return new(TRight.Poly / TArgument.Poly, Measure.Of<TLeft>());
            }
            return new(Polynomial.One, Measure.Of<Quotient<Product<TLeft, TRight>, TArgument>>());
        }
    }

    public sealed class Div<TNominator, TDenominator> : ICompute
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        public static Result Multiply<TArgument>() where TArgument : IMeasure
        {
            if (TNominator.Rank<TArgument>() == One) {
                return new(TNominator.Poly / TArgument.Poly, Measure.Of<Quotient<Power<Square, TNominator>, TDenominator>>());
            }
            if (TDenominator.Rank<TArgument>() == One) {
                return new(TArgument.Poly / TDenominator.Poly, Measure.Of<TNominator>());
            }
            return new(Polynomial.One, Measure.Of<Product<Quotient<TNominator, TDenominator>, TArgument>>());
        }
        public static Result Divide<TArgument>() where TArgument : IMeasure
        {
            if (TNominator.Rank<TArgument>() == One) {
                return new(TNominator.Poly / TArgument.Poly, Measure.Of<Quotient<Identity, TDenominator>>());
            }
            if (TDenominator.Rank<TArgument>() == One) {
                return new(TArgument.Poly / TDenominator.Poly, Measure.Of<Quotient<TNominator, Power<Square, TDenominator>>>());
            }
            return new(Polynomial.One, Measure.Of<Quotient<TNominator, Product<TDenominator, TArgument>>>());
        }
    }
    public sealed class HighOrder<THigher, TLinear> : ICompute
        where THigher : IMeasure
        where TLinear : IMeasure
    {
        private static readonly Int32 self = ExponentOf(THigher.Rank<TLinear>());
        public static Result Multiply<TArgument>() where TArgument : IMeasure
        {
            Int32 target = self + ExponentOf(TLinear.Rank<TArgument>());
            Measure? measure = Pow<TLinear>(target);
            if (measure is null) {
                return new(Polynomial.One, Measure.Of<Product<THigher, TArgument>>());
            }
            Polynomial conversion = TArgument.Poly * THigher.Poly / TLinear.Poly.Pow(target);
            return new(in conversion, measure);
        }
        public static Result Divide<TArgument>() where TArgument : IMeasure
        {
            Int32 target = self - ExponentOf(TLinear.Rank<TArgument>());
            Measure? measure = Pow<TLinear>(target);
            if (measure is null) {
                return new(Polynomial.One, Measure.Of<Quotient<THigher, TArgument>>());
            }
            Polynomial conversion = THigher.Poly / (TLinear.Poly.Pow(target) * TArgument.Poly);
            return new(in conversion, measure);
        }
    }
}

file static class Convenience
{
    public const Int32 NoRank = Int16.MaxValue;
    public static Int32 ExponentOf(Rank rank) => rank switch {
        Inverse => -1,
        Zero => 0,
        One => 1,
        Two => 2,
        Three => 3,
        _ => NoRank
    };
    public static Measure? Pow<TLinear>(Int32 exp)
        where TLinear : IMeasure => exp switch {
            3 => Measure.Of<Power<Cubic, TLinear>>(),
            2 => Measure.Of<Power<Square, TLinear>>(),
            1 => Measure.Of<TLinear>(),
            0 => Measure.Of<Identity>(),
            -1 => Measure.Of<Quotient<Identity, TLinear>>(),
            -2 => Measure.Of<Quotient<Identity, Power<Square, TLinear>>>(),
            -3 => Measure.Of<Quotient<Identity, Power<Cubic, TLinear>>>(),
            _ => null
        };
}
