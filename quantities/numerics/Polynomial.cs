using System.Numerics;
using static Quantities.Numerics.Algorithms;

namespace Quantities.Numerics;

internal readonly record struct Polynomial : IEquatable<Polynomial>
    , IEqualityOperators<Polynomial, Polynomial, Boolean>
    , IMultiplyOperators<Polynomial, Double, Double>
    , IMultiplyOperators<Polynomial, Polynomial, Polynomial>
    , IDivisionOperators<Polynomial, Polynomial, Polynomial>
    , IDivisionOperators<Polynomial, Double, Double>
{
    public static Polynomial NoOp { get; } = new();
    private readonly Double nominator, denominator, offset;
    public Polynomial() => (this.nominator, this.denominator, this.offset) = (1, 1, 0);
    private Polynomial(in Double nominator, in Double denominator, in Double offset)
    {
        (this.nominator, this.denominator, this.offset) = (nominator, denominator, offset);
    }
    public static Polynomial Of(Transformation transformation)
    {
        var (n, d, offset) = transformation;
        (n, d) = Simplify(n, d);
        return d >= 0 ? new(n, d, offset) : new(-n, -d, offset);
        static (Double n, Double d) Simplify(Double n, Double d)
        {
            if (Double.IsInteger(n) && Double.IsInteger(d) && Double.Abs(Double.MaxMagnitude(n, d)) < Int64.MaxValue) {
                Int64 gcd = Gcd((Int64)n, (Int64)d);
                return gcd <= 1 ? (n, d) : (n / gcd, d / gcd);
            }
            return (n, d);
        }
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
        => Double.FusedMultiplyAdd(left.nominator, right, left.denominator * left.offset) / left.denominator;

    public static Polynomial operator *(Polynomial left, Polynomial right)
        => new(left.nominator * right.nominator, left.denominator * right.denominator, left * right.offset);

    public static Polynomial operator /(Polynomial left, Polynomial right)
        => new(left.nominator * right.denominator, left.denominator * right.nominator, right / left.offset);

    public static Double operator /(Polynomial left, Double right)
        => left.denominator * (right - left.offset) / left.nominator;

    public override String ToString()
    {
        var (fraction, offset) = Split(in this.nominator, in this.denominator, in this.offset);
        return $"f(x) = {fraction}{offset}";

        static (String fraction, String offset) Split(in Double n, in Double d, in Double o)
        {
            if (d == 0d || n == 0) {
                return (d == 0d ? n >= 0 ? "∞" : "-∞" : o.ToString("g4"), String.Empty);
            }
            var fraction = (n, d) switch {
                (1, 1) => "x",
                (-1, 1) => "-x",
                (-1, _) => $"-x/{d:g4}",
                (_, 1) => $"{n:g4}*x",
                _ => $"{n:g4}*x/{d:g4}",
            };
            var offset = o switch {
                < 0d => $" - {-o:g4}",
                > 0d => $" + {o:g4}",
                _ => String.Empty,
            };
            return (fraction, offset);
        }
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
