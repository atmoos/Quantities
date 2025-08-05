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

    // ToDo: Include this in the deserializer as a special case.
    public static void Write(IWriter writer, Int32 exponent) => writer.Write(name, Representation);
}

internal readonly struct Si<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TUnit>));
    public static Polynomial Poly => Polynomial.One;
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Si<TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Si<TUnit>>>();
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);
}
internal readonly struct Si<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Si<TPrefix, TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix>();
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Si<TPrefix, TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Si<TPrefix, TUnit>>>();
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write<TPrefix>(writer, exponent);
}
internal readonly struct Metric<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Metric<TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Metric<TUnit>>>();
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);
}
internal readonly struct Metric<TPrefix, TUnit> : IMeasure<TUnit>, ILinear
    where TPrefix : IPrefix
    where TUnit : IMetricUnit, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Metric<TPrefix, TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TPrefix, TUnit>();
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Metric<TPrefix, TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Metric<TPrefix, TUnit>>>();
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write<TPrefix>(writer, exponent);
}
internal readonly struct Imperial<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : IImperialUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new(nameof(Imperial<TUnit>));
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Imperial<TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Imperial<TUnit>>>();
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);
}
internal readonly struct NonStandard<TUnit> : IMeasure<TUnit>, ILinear
    where TUnit : INonStandardUnit, ITransform, IRepresentable, IDimension
{
    private static readonly Serializer<TUnit> serializer = new("any");
    public static Polynomial Poly { get; } = Polynomial.Of<TUnit>();
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<NonStandard<TUnit>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<NonStandard<TUnit>>>();
    public static String Representation => TUnit.Representation;
    public static void Write(IWriter writer, Int32 exponent) => serializer.Write(writer, exponent);
}

internal readonly struct Inverse<TSelf> : IMeasure, ILinear
    where TSelf : IMeasure
{
    public static Dimension D { get; } = TSelf.D.Pow(-1);
    public static Polynomial Poly { get; } = TSelf.Poly.Pow(-1);
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<Inverse<TSelf>>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<TSelf>();
    public static String Representation { get; } = $"{TSelf.Representation}{Tools.ExpToString(-TSelf.D.E)}";
    public static void Write(IWriter writer, Int32 exponent) => TSelf.Write(writer, exponent);
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

    private static String Rep() => (TLeft.D.E, TRight.D.E) switch {
        ( >= 0, < 0) => $"{TLeft.Representation}{division}{InvertedRep<TRight>()}",
        ( < 0, >= 0) => $"{TRight.Representation}{division}{InvertedRep<TLeft>()}",
        _ => $"{TLeft.Representation}{zeroWidthNonJoiner}{TRight.Representation}",
    };
}

internal readonly struct Alias<TAlias, TLinear> : IMeasure
    where TAlias : IMeasure
    where TLinear : IMeasure, ILinear
{
    static Dimension IMeasure.D { get; } = TAlias.D;
    public static Polynomial Poly => TAlias.Poly;
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<TLinear>().Inject<TAlias>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => inject.Inject<Inverse<Alias<TAlias, TLinear>>>();
    public static String Representation => TAlias.Representation;
    public static void Write(IWriter writer, Int32 exponent) => TAlias.Write(writer, exponent >= 0 ? 1 : -1);
}

internal readonly struct Power<TExp, TLinear> : IMeasure
    where TExp : IExponent
    where TLinear : IMeasure
{
    static Dimension IMeasure.D { get; } = TLinear.D.Pow(TExp.E);
    public static Polynomial Poly { get; } = TLinear.Poly.Pow(TExp.E);
    public static IVisitor InjectLinear(IVisitor inject) => inject.Inject<TLinear>();
    public static TResult InjectInverse<TResult>(IInject<TResult> inject) => TExp.Invert(new PowerInverse<TResult>(inject));
    public static String Representation { get; } = $"{TLinear.Representation}{Tools.ExpToString(TExp.E)}";
    public static void Write(IWriter writer, Int32 exponent) => TLinear.Write(writer, exponent);

    private sealed class PowerInverse<TResult>(IInject<TResult> inject) : IInjectExponent<TResult>
    {
        public TResult Inject<TExponent>() where TExponent : IExponent => inject.Inject<Power<TExponent, TLinear>>();
    }
}

file static class Convenience
{
    private static readonly Rep readInverse = new();
    public static String InvertedRep<TMeasure>()
        where TMeasure : IMeasure
    {
        return TMeasure.InjectInverse(readInverse);
    }

    private sealed class Rep : IInject<String>
    {
        public String Inject<TMeasure>() where TMeasure : IMeasure => TMeasure.Representation;
    }
}
