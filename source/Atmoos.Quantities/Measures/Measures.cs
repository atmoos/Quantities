using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Core.Serialization;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Units;
using static Atmoos.Quantities.Measures.Convenience;

namespace Atmoos.Quantities.Measures;

internal readonly struct Identity : IMeasure, ILinear
{
    private static readonly String name = nameof(Identity).ToLowerInvariant();
    static Dimension IMeasure.D => Unit.Identity;
    public static Polynomial Poly => Polynomial.One;

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Identity>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Identity>();

    public static String Representation { get; } = Unit.Identity.ToString();

    public static void Write(IWriter writer, Int32 exponent) => writer.Write(name, Representation);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Inject<Identity>();
}

internal readonly struct Si<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.One;

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Si<TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Si<TUnit>, Negative<One>>>();

    public static String Representation => TUnit.Representation;

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Si<TUnit>>(exponent);
}

internal readonly struct Si<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TPrefix, TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix>();

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Si<TPrefix, TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Si<TPrefix, TUnit>, Negative<One>>>();

    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write<TPrefix>(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Si<TPrefix, TUnit>>(exponent);
}

internal readonly struct Metric<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Metric<TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Metric<TUnit>, Negative<One>>>();

    public static String Representation => TUnit.Representation;

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Metric<TUnit>>(exponent);
}

internal readonly struct Metric<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix, TUnit>();

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Metric<TPrefix, TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Metric<TPrefix, TUnit>, Negative<One>>>();

    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write<TPrefix>(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Metric<TPrefix, TUnit>>(exponent);
}

internal readonly struct Imperial<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Imperial<TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Imperial<TUnit>, Negative<One>>>();

    public static String Representation => TUnit.Representation;

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Imperial<TUnit>>(exponent);
}

internal readonly struct NonStandard<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : INonStandardUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<NonStandard<TUnit>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<NonStandard<TUnit>, Negative<One>>>();

    public static String Representation => TUnit.Representation;

    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<NonStandard<TUnit>>(exponent);
}

internal readonly struct Invertible<TSelf, TInverse> : IMeasure, ILinear
    where TSelf : IMeasure
    where TInverse : IMeasure, ILinear
{
    public static Dimension D => TSelf.D;
    public static Polynomial Poly => TSelf.Poly;

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Invertible<TSelf, TInverse>>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<TInverse>();

    public static String Representation => TSelf.Representation;

    // ToDo: Inversion of the exponent is not really what we want here. This must be refined.
    public static void Write(IWriter writer, Int32 exponent) => TSelf.Write(writer, -exponent);

    public static IVisitor Power(IVisitor inject, Int32 exponent) =>
        exponent switch {
            >= 0 => inject.Raise<Invertible<TSelf, TInverse>>(-exponent),
            < 0 => inject.Raise<TInverse>(exponent)
        };
}

internal readonly struct Product<TLeft, TRight> : IMeasure
    where TLeft : IMeasure
    where TRight : IMeasure
{
    private const String division = "/";
    private const String zeroWidthNonJoiner = "\u200C"; // https://en.wikipedia.org/wiki/Zero-width_non-joiner
    static Dimension IMeasure.D { get; } = TLeft.D * TRight.D;
    public static Polynomial Poly { get; } = TLeft.Poly * TRight.Poly;

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<TLeft>().Inject<TRight>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => Arithmetic<TLeft>.Invert<TResult, TRight>(inject);

    public static String Representation { get; } = Rep();

    public static void Write(IWriter writer, Int32 exponent)
    {
        writer.StartArray("measures");
        writer.Start();
        TLeft.Write(writer, exponent * TLeft.D.E);
        writer.End();
        writer.Start();
        TRight.Write(writer, exponent * TRight.D.E);
        writer.End();
        writer.EndArray();
    }

    public static IVisitor Power(IVisitor inject, Int32 exponent) => inject.Raise<Product<TLeft, TRight>>(exponent);

    private static String Rep() =>
        (TLeft.D.E, TRight.D.E) switch {
            ( >= 0, < 0) => $"{TLeft.Representation}{division}{InvertedRep<TRight>()}",
            ( < 0, >= 0) => $"{TRight.Representation}{division}{InvertedRep<TLeft>()}",
            _ => $"{TLeft.Representation}{zeroWidthNonJoiner}{TRight.Representation}"
        };
}

internal readonly struct Alias<TAlias, TLinear> : IMeasure
    where TAlias : IMeasure
    where TLinear : IMeasure, ILinear
{
    static Dimension IMeasure.D => TAlias.D;
    public static Polynomial Poly => TAlias.Poly;

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<TLinear>().Inject<TAlias>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Power<Alias<TAlias, TLinear>, Negative<One>>>();

    public static String Representation => TAlias.Representation;

    public static void Write(IWriter writer, Int32 exponent) => TAlias.Write(writer, exponent >= 0 ? 1 : -1);

    public static IVisitor Power(IVisitor inject, Int32 exponent) => exponent == TAlias.D.E ? inject.Inject<Alias<TAlias, TLinear>>() : inject.Raise<TLinear>(exponent);
}

internal readonly struct Power<TLinear, TExp> : IMeasure
    where TExp : INumber
    where TLinear : IMeasure
{
    private static readonly Dimension d = TLinear.D.Pow(TExp.Value);
    static Dimension IMeasure.D => d;
    public static Polynomial Poly { get; } = TLinear.Poly.Pow(TExp.Value);

    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<TLinear>();

    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => TExp.Negate(new PowerInverse<TLinear, TResult>(inject));

    public static String Representation { get; } = $"{TLinear.Representation}{Tools.ToExponent(TExp.Value)}";

    public static void Write(IWriter writer, Int32 exponent) => TLinear.Write(writer, exponent);

    static IVisitor IMeasure.Power(IVisitor inject, Int32 exponent) => inject.Raise<TLinear>(exponent);
}

file sealed class PowerInverse<TLinear, TResult>(IInject<TResult> inject) : IInjectNumber<TResult>
    where TLinear : IMeasure
{
    public TResult Inject<TExponent>()
        where TExponent : INumber => TExponent.Value == 1 ? inject.Inject<TLinear>() : inject.Inject<Power<TLinear, TExponent>>();
}

file static class Convenience
{
    private static readonly Rep readInverse = new();

    public static String InvertedRep<TMeasure>()
        where TMeasure : IMeasure => TMeasure.InjectInverse(readInverse);

    private sealed class Rep : IInject<String>
    {
        public String Inject<TMeasure>()
            where TMeasure : IMeasure => TMeasure.Representation;
    }

    public static IVisitor Raise<TMeasure>(this IVisitor visitor, Int32 exponent)
        where TMeasure : IMeasure =>
        exponent switch {
            5 => visitor.Inject<Power<TMeasure, Five>>(),
            4 => visitor.Inject<Power<TMeasure, Four>>(),
            3 => visitor.Inject<Power<TMeasure, Three>>(),
            2 => visitor.Inject<Power<TMeasure, Two>>(),
            1 => visitor.Inject<TMeasure>(),
            0 => visitor.Inject<Identity>(),
            -1 => TMeasure.InjectInverse(visitor),
            -2 => visitor.Inject<Power<TMeasure, Negative<Two>>>(),
            -3 => visitor.Inject<Power<TMeasure, Negative<Three>>>(),
            -4 => visitor.Inject<Power<TMeasure, Negative<Four>>>(),
            -5 => visitor.Inject<Power<TMeasure, Negative<Five>>>(),
            _ => throw new InvalidOperationException($"Exponent '{exponent}' is not supported.")
        };
}
