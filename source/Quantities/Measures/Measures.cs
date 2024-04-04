using Quantities.Core.Numerics;
using Quantities.Core.Serialization;
using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units;

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

// Try making this a class scoped to this file only.
internal readonly struct Identity : IMeasure, ILinear
{
    private static readonly String name = nameof(Identity).ToLowerInvariant();
    static Dimension IMeasure.D => Unit.Identity;
    public static Polynomial Poly => Polynomial.One;
    public static Result Inverse => Result<Identity>.Unit;
    public static String Representation { get; } = Unit.Identity.ToString();
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => TMeasure.Inverse;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => Result<TMeasure>.Unit;
    public static void Write(IWriter writer) => writer.Write(name, Representation);

    private static class Result<TMeasure>
        where TMeasure : IMeasure
    {
        public static Result Unit { get; } = new(Polynomial.One, Measure.Of<TMeasure>());
    }
}

internal readonly struct Si<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.One;
    public static Result Inverse => SafeInverse<Si<TUnit>>.Value;
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
    public static Result Inverse => SafeInverse<Si<TPrefix, TUnit>>.Value;
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
    public static Result Inverse => SafeInverse<Metric<TUnit>>.Value;
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
    public static Result Inverse => SafeInverse<Metric<TPrefix, TUnit>>.Value;
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
    public static Result Inverse => SafeInverse<Imperial<TUnit>>.Value;
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<Imperial<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}
internal readonly struct NonStandard<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : INonStandardUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static Result Inverse => SafeInverse<NonStandard<TUnit>>.Value;
    public static String Representation => TUnit.Representation;
    public static Result Multiply<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Product;
    public static Result Divide<TMeasure>()
        where TMeasure : IMeasure => ScalarOps<NonStandard<TUnit>, TMeasure>.Quotient;
    public static void Write(IWriter writer) => serializer.Write(writer);
}

internal readonly struct Inverse<TSelf> : IMeasure, ILinear
    where TSelf : IMeasure
{
    public static Dimension D { get; } = TSelf.D.Pow(-1);
    public static Polynomial Poly { get; } = TSelf.Poly.Pow(-1);
    static Result IMeasure.Inverse { get; } = new(Polynomial.One, Measure.Of<TSelf>());
    public static String Representation { get; } = $"{TSelf.Representation}{Tools.ExpToString(-TSelf.D.E)}";
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => ((Measure)ScalarOps<TSelf, TMeasure>.Product).Invert();
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => ScalarOps<TMeasure, TSelf>.Quotient;

    // ToDo: This should not simply write out self!
    public static void Write(IWriter writer) => TSelf.Write(writer);
}

internal readonly struct Invertible<TSelf, TInverse> : IMeasure, ILinear
    where TSelf : IMeasure
    where TInverse : IMeasure, ILinear
{
    public static Dimension D => TSelf.D;
    public static Polynomial Poly => TSelf.Poly;
    static Result IMeasure.Inverse { get; } = new(TInverse.Poly / TSelf.Poly, Measure.Of<TInverse>());
    public static String Representation => TSelf.Representation;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => ScalarOps<TSelf, TMeasure>.Quotient;
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => ScalarOps<TSelf, TMeasure>.Product;
    public static void Write(IWriter writer) => TSelf.Write(writer);
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    private const String zeroWidthNonJoiner = "\u200C"; // https://en.wikipedia.org/wiki/Zero-width_non-joiner
    static Dimension IMeasure.D { get; } = TLeft.D * TRight.D;
    public static Polynomial Poly { get; } = TLeft.Poly * TRight.Poly;
    public static Result Inverse => SafeInverse<Product<TLeft, TRight>>.Value;
    public static String Representation { get; } = $"{TLeft.Representation}{zeroWidthNonJoiner}{TRight.Representation}";
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => ProductOps<TLeft, TRight, TMeasure>.Quotient;
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
    private static readonly String representation = $"{TNominator.Representation}/{TDenominator.Representation}";
    static Dimension IMeasure.D { get; } = TNominator.D / TDenominator.D;
    public static Polynomial Poly { get; } = TNominator.Poly / TDenominator.Poly;
    public static Result Inverse { get; } = new(Polynomial.One, Measure.Of<Quotient<TDenominator, TNominator>>());
    public static String Representation => representation;
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Product;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => QuotientOps<TNominator, TDenominator, TMeasure>.Quotient;
    public static void Write(IWriter writer)
    {
        writer.Start("quotient");
        TNominator.Write(writer);
        TDenominator.Write(writer);
        writer.End();
    }
}

internal readonly struct Alias<TAlias, TLinear> : IMeasure
    where TAlias : IMeasure
    where TLinear : IMeasure, ILinear
{
    static Dimension IMeasure.D { get; } = TAlias.D;
    public static Polynomial Poly => TAlias.Poly;
    public static Result Inverse => SafeInverse<Alias<TAlias, TLinear>>.Value;
    public static String Representation => TAlias.Representation;
    public static Result Divide<TMeasure>() where TMeasure : IMeasure => HigherOrderOps<TAlias, TLinear, TMeasure>.Quotient;
    public static Result Multiply<TMeasure>() where TMeasure : IMeasure => HigherOrderOps<TAlias, TLinear, TMeasure>.Product;
    public static void Write(IWriter writer) => TAlias.Write(writer);
}

internal readonly struct Power<TDim, TLinear> : IMeasure
    where TDim : IExponent
    where TLinear : IMeasure
{
    private static readonly String dimension = typeof(TDim).Name.ToLowerInvariant();
    static Dimension IMeasure.D { get; } = TLinear.D.Pow(TDim.E);
    public static Polynomial Poly { get; } = TLinear.Poly.Pow(TDim.E);
    public static Result Inverse { get; } = TDim.Invert(new PowerInverse<TLinear>());
    public static String Representation { get; } = $"{TLinear.Representation}{Tools.ExpToString(TDim.E)}";
    public static Result Multiply<TOtherMeasure>() where TOtherMeasure : IMeasure => HigherOrderOps<Power<TDim, TLinear>, TLinear, TOtherMeasure>.Product;
    public static Result Divide<TOtherMeasure>() where TOtherMeasure : IMeasure => HigherOrderOps<Power<TDim, TLinear>, TLinear, TOtherMeasure>.Quotient;
    public static void Write(IWriter writer)
    {
        writer.Start(dimension);
        TLinear.Write(writer);
        writer.End();
    }
}

file sealed class PowerInverse<TLinear> : IInjectExponent<Result>
    where TLinear : IMeasure
{
    public Result Inject<TExponent>() where TExponent : IExponent => new(Polynomial.One, Measure.Of<Power<TExponent, TLinear>>());
}

file static class SafeInverse<TMeasure>
    where TMeasure : IMeasure
{
    public static Result Value { get; } = new(Polynomial.One, Measure.Of<Inverse<TMeasure>>());
}

file sealed class ScalarOps<TScalar, TArgument> : IOps
    where TScalar : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Scalar<TScalar>.Multiply<TArgument>();
    public static Result Quotient { get; } = Scalar<TScalar>.Divide<TArgument>();
}

file sealed class ProductOps<TLeft, TRight, TArgument> : IOps
    where TLeft : IMeasure
    where TRight : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Prod<TLeft, TRight>.Multiply<TArgument>();
    public static Result Quotient { get; } = Prod<TLeft, TRight>.Divide<TArgument>();
}

file sealed class QuotientOps<TNominator, TDenominator, TArgument> : IOps
    where TNominator : IMeasure
    where TDenominator : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = Div<TNominator, TDenominator>.Multiply<TArgument>();
    public static Result Quotient { get; } = Div<TNominator, TDenominator>.Divide<TArgument>();
}

file sealed class HigherOrderOps<THigher, TLinear, TArgument> : IOps
    where THigher : IMeasure
    where TLinear : IMeasure
    where TArgument : IMeasure
{
    public static Result Product { get; } = HighOrder<THigher, TLinear>.Multiply<TArgument>();
    public static Result Quotient { get; } = HighOrder<THigher, TLinear>.Divide<TArgument>();
}

// Everything in here is allowed to be comparatively inefficient, as it is
// only ever executed once during the lifetime of any dependent app.

file sealed class Scalar<TSelf> : ICompute
    where TSelf : IMeasure
{
    public static Result Multiply<TArgument>()
        where TArgument : IMeasure
    {
        if (TArgument.D is Product) {
            return TArgument.Multiply<TSelf>();
        }
        var product = TSelf.D * TArgument.D;
        return product switch {
            Unit => new(TSelf.Poly * TArgument.Poly, Measure.Of<Identity>()),
            Scalar s when s.E == 1 => new Result(TArgument.Poly, Measure.Of<TSelf>()),
            Scalar s when s.E == 2 => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<Power<Square, TSelf>>()),
            Scalar s when s.E == 3 => new Result(TArgument.Poly / TSelf.Poly, Measure.Of<Power<Cubic, TSelf>>()),
            _ => new Result(Polynomial.One, Measure.Of<Product<TSelf, TArgument>>())
        };
    }

    public static Result Divide<TArgument>()
        where TArgument : IMeasure
    {
        var quotient = TSelf.D / TArgument.D;
        return quotient switch {
            Unit => new Result(TSelf.Poly / TArgument.Poly, Measure.Of<Identity>()),
            Scalar s when s.E == 1 => new Result(TArgument.Poly.Pow(-1), Measure.Of<TSelf>()),
            Scalar s when s.E == 2 => new Result(TSelf.Poly / TArgument.Poly, Measure.Of<Power<Square, TSelf>>()),
            Scalar s when s.E == 3 => new Result(TSelf.Poly / TArgument.Poly, Measure.Of<Power<Cubic, TSelf>>()),
            _ => new Result(Polynomial.One, Measure.Of<Quotient<TSelf, TArgument>>())
        };
    }
}

file sealed class Prod<TLeft, TRight> : ICompute
    where TLeft : IMeasure
    where TRight : IMeasure
{
    public static Result Multiply<TArgument>() where TArgument : IMeasure
    {
        var product = TLeft.D * TRight.D * TArgument.D;
        var polyProduct = TLeft.Poly * TRight.Poly * TArgument.Poly;
        if (product is Unit) {
            return new(polyProduct, Measure.Of<Identity>());
        }
        Measure? measure;
        if (product is Scalar scalar && (measure = scalar.As<TLeft>()) != null) {
            return new(polyProduct / TLeft.Poly, measure);
        }
        return new(Polynomial.One, Measure.Of<Product<Product<TLeft, TRight>, TArgument>>());
    }
    public static Result Divide<TArgument>() where TArgument : IMeasure
    {
        var quotient = TLeft.D * TRight.D / TArgument.D;
        var polyQuotient = TLeft.Poly * TRight.Poly / TArgument.Poly;
        if (quotient is Unit) {
            return new(polyQuotient, Measure.Of<Identity>());
        }
        Measure? measure;
        if (quotient is Scalar scalar && (measure = scalar.As<TLeft>()) != null) {
            return new(polyQuotient / TLeft.Poly, measure);
        }
        return new(Polynomial.One, Measure.Of<Quotient<Product<TLeft, TRight>, TArgument>>());
    }
}

file sealed class Div<TNominator, TDenominator> : ICompute
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public static Result Multiply<TArgument>() where TArgument : IMeasure => Product<TNominator, TArgument>.Divide<TDenominator>();
    public static Result Divide<TArgument>() where TArgument : IMeasure
    {
        var quotient = TNominator.D / (TDenominator.D * TArgument.D);
        var polyQuotient = TNominator.Poly / (TDenominator.Poly * TArgument.Poly);
        if (quotient is Unit) {
            return new(polyQuotient, Measure.Of<Identity>());
        }
        Measure? measure;
        if (quotient is Scalar scalar && (measure = scalar.As<TNominator>()) != null) {
            return new(polyQuotient / TNominator.Poly, measure);
        }
        return new(Polynomial.One, Measure.Of<Quotient<TNominator, Product<TDenominator, TArgument>>>());
    }
}
file sealed class HighOrder<THigher, TLinear> : ICompute
    where THigher : IMeasure
    where TLinear : IMeasure
{
    public static Result Multiply<TArgument>() where TArgument : IMeasure
    {
        Dimension target = THigher.D * TArgument.D;
        Measure? measure = target.As<TLinear>();
        if (measure is null) {
            return new(Polynomial.One, Measure.Of<Product<THigher, TArgument>>());
        }
        Polynomial conversion = TArgument.Poly * THigher.Poly / TLinear.Poly.Pow(target.E);
        return new(in conversion, measure);
    }
    public static Result Divide<TArgument>() where TArgument : IMeasure
    {
        Dimension target = THigher.D / TArgument.D;
        Measure? measure = target.As<TLinear>();
        if (measure is null) {
            return new(Polynomial.One, Measure.Of<Quotient<THigher, TArgument>>());
        }
        Polynomial conversion = THigher.Poly / (TLinear.Poly.Pow(target.E) * TArgument.Poly);
        return new(in conversion, measure);
    }
}

file static class Convenience
{
    public static Measure? As<TLinear>(this Dimension d)
        where TLinear : IMeasure => d.E switch {
            3 => Measure.Of<Power<Cubic, TLinear>>(),
            2 => Measure.Of<Power<Square, TLinear>>(),
            1 => Measure.Of<TLinear>(),
            0 => Measure.Of<Identity>(),
            -1 => Measure.Of<Inverse<TLinear>>(),
            -2 => Measure.Of<Power<InverseExp<Square>, TLinear>>(),
            -3 => Measure.Of<Power<InverseExp<Cubic>, TLinear>>(),
            _ => null
        };
}
