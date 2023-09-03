using System.Numerics;

namespace Quantities.Numerics;

internal readonly struct Polynomial
    : IMultiplyOperators<Polynomial, Double, Double>
    , IMultiplyOperators<Polynomial, Polynomial, Polynomial>
    , IDivisionOperators<Polynomial, Polynomial, Polynomial>
{
    public static Polynomial NoOp { get; } = new();
    private readonly Double nominator, denominator, offset;
    public Polynomial() => (this.nominator, this.denominator, this.offset) = (1, 1, 0);
    private Polynomial(in Double nominator, in Double denominator, in Double offset)
    {
        this.nominator = nominator;
        this.denominator = denominator;
        this.offset = offset;
    }
    public readonly T Evaluate<T>(in T value) where T :
    IAdditionOperators<T, Double, T>,
    ISubtractionOperators<T, Double, T>,
    IMultiplyOperators<T, Double, T>,
    IDivisionOperators<T, Double, T> => value * this.nominator / this.denominator + this.offset;
    public readonly T Inverse<T>(in T value) where T :
    IAdditionOperators<T, Double, T>,
    ISubtractionOperators<T, Double, T>,
    IMultiplyOperators<T, Double, T>,
    IDivisionOperators<T, Double, T> => (value - this.offset) * this.denominator / this.nominator;
    public static Polynomial Of(Transformation transformation)
    {
        var (nominator, denominator, offset) = transformation;
        return new(in nominator, in denominator, in offset);
    }
    public static Polynomial Of<TTransform>()
        where TTransform : ITransform => Cache<TTransform>.Polynomial;
    public static Polynomial Of<TSecond, TFirst>()
        where TFirst : ITransform where TSecond : ITransform => Cache<TFirst, TSecond>.Polynomial;
    public static Polynomial Conversion<TFrom, TTo>()
        where TFrom : ITransform where TTo : ITransform => Converter<TFrom, TTo>.Polynomial;
    public static Double Convert<TFrom, TTo>(in Double value)
        where TFrom : ITransform where TTo : ITransform => Converter<TFrom, TTo>.Polynomial.Evaluate(in value);

    public static Double operator *(Polynomial left, Double right)
    {
        return right * left.nominator / left.denominator + left.offset;
    }

    public static Polynomial operator *(Polynomial left, Polynomial right)
    {
        var offset = right.nominator * (left.offset + right.offset) / right.denominator;
        return new(left.nominator * right.nominator, left.denominator * right.denominator, in offset);
    }

    public static Polynomial operator /(Polynomial left, Polynomial right)
    {
        var offset = right.denominator * (left.offset - right.offset) / right.nominator;
        return new(left.nominator * right.denominator, left.denominator * right.nominator, in offset);
    }

    private static class Cache<T>
        where T : ITransform
    {
        public static readonly Polynomial Polynomial = Of(T.ToSi(new Transformation()));
    }

    private static class Cache<First, Second>
    where First : ITransform
    where Second : ITransform
    {
        public static readonly Polynomial Polynomial = Of(Second.ToSi(First.ToSi(new Transformation())));
    }

    private static class Converter<TFrom, TTo>
        where TFrom : ITransform
        where TTo : ITransform
    {
        public static readonly Polynomial Polynomial = Of<TFrom>() / Of<TTo>();
    }
}