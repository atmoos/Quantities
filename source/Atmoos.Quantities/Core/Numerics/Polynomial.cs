using System.Numerics;

namespace Atmoos.Quantities.Core.Numerics;

internal readonly record struct Polynomial
    : IEquatable<Polynomial>,
        IEqualityOperators<Polynomial, Polynomial, Boolean>,
        IMultiplicativeIdentity<Polynomial, Polynomial>,
        IMultiplyOperators<Polynomial, Double, Double>,
        IMultiplyOperators<Polynomial, Polynomial, Polynomial>,
        IDivisionOperators<Polynomial, Polynomial, Polynomial>,
        IDivisionOperators<Polynomial, Double, Double>
{
    public static Polynomial One { get; } = new();
    static Polynomial IMultiplicativeIdentity<Polynomial, Polynomial>.MultiplicativeIdentity => One;
    private readonly Double nominator,
        denominator,
        offset;

    public Polynomial() => (this.nominator, this.denominator, this.offset) = (1, 1, 0);

    private Polynomial(in Double nominator, in Double denominator, in Double offset)
    {
        (this.nominator, this.denominator, this.offset) = (nominator, denominator, offset);
    }

    internal Polynomial Simplify()
    {
        var (n, d) = Algorithms.Simplify(in this.nominator, in this.denominator);
        return new(in n, in d, this.offset);
    }

    public Polynomial Pow(Int32 exponent) => Algorithms.Pow(in this, exponent);

    public static Polynomial Of(Transformation transformation)
    {
        var (n, d, offset) = transformation;
        (n, d) = Algorithms.Simplify(in n, in d);
        return new(in n, in d, in offset);
    }

    public static Polynomial Of<TTransform>()
        where TTransform : ITransform => Cache<TTransform>.Polynomial;

    public static Polynomial Of<TSecond, TFirst>()
        where TFirst : ITransform
        where TSecond : ITransform => Cache<TFirst, TSecond>.Polynomial;

    public static Double operator *(Polynomial left, Double right) => Double.FusedMultiplyAdd(left.nominator, right, left.denominator * left.offset) / left.denominator;

    // FYI: Vector multiplication leads to performance degradation. Not worth it here...
    public static Polynomial operator *(Polynomial left, Polynomial right) => new(left.nominator * right.nominator, left.denominator * right.denominator, left * right.offset);

    public static Polynomial operator /(Polynomial left, Polynomial right) => new(left.nominator * right.denominator, left.denominator * right.nominator, right / left.offset);

    public static Double operator /(Polynomial left, Double right) => left.denominator * (right - left.offset) / left.nominator;

    internal void Deconstruct(out Double nominator, out Double denominator, out Double offset)
    {
        (nominator, denominator, offset) = (this.nominator, this.denominator, this.offset);
    }

    public override String ToString()
    {
        var (n, d, o) = Simplify();
        var (fraction, offset) = Split(in n, in d, in o);
        return $"p(x) = {fraction}{offset}";

        static (String fraction, String offset) Split(in Double n, in Double d, in Double o)
        {
            if (d == 0d || n == 0d) {
                return (
                    d == 0d
                        ? n >= 0d
                            ? "∞"
                            : "-∞"
                        : o.ToString("g4"),
                    String.Empty
                );
            }
            var fraction = (n, d) switch {
                (1, 1) => "x",
                (1, _) => $"x/{d:g4}",
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
}

file static class Cache<T>
    where T : ITransform
{
    public static readonly Polynomial Polynomial = Polynomial.Of(T.ToSi(new Transformation()));
}

file static class Cache<First, Second>
    where First : ITransform
    where Second : ITransform
{
    public static readonly Polynomial Polynomial = Polynomial.Of(Second.ToSi(First.ToSi(new Transformation())));
}
