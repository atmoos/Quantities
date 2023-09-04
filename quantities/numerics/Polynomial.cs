using System.Numerics;

namespace Quantities.Numerics;

internal readonly struct Polynomial
    : IMultiplyOperators<Polynomial, Double, Double>
    , IMultiplyOperators<Polynomial, Polynomial, Polynomial>
    , IDivisionOperators<Polynomial, Polynomial, Polynomial>
    , IDivisionOperators<Polynomial, Double, Double>
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
        where TFrom : ITransform where TTo : ITransform => Converter<TFrom, TTo>.Polynomial * value;

    public static Double operator *(Polynomial left, Double right)
    {
        return Double.FusedMultiplyAdd(left.nominator, right, left.denominator * left.offset) / left.denominator;
    }

    public static Polynomial operator *(Polynomial left, Polynomial right)
    {
        var scaledOffset = Double.FusedMultiplyAdd(left.nominator, right.offset, left.denominator * left.offset) / left.denominator;
        return new(left.nominator * right.nominator, left.denominator * right.denominator, in scaledOffset);
    }

    public static Polynomial operator /(Polynomial left, Polynomial right)
    {
        var offset = right.denominator * (left.offset - right.offset) / right.nominator;
        return new(left.nominator * right.denominator, left.denominator * right.nominator, in offset);
    }

    public static Double operator /(Polynomial left, Double right)
    {
        return (right - left.offset) * left.denominator / left.nominator;
    }

    public override String ToString() => $"f(x) = {this.nominator}*x/{this.denominator} + {this.offset}";

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