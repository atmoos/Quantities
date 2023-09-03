using System.Numerics;

namespace Quantities.Numerics;

internal readonly struct Polynomial
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
    public T Evaluate<T>(in T value) where T :
    IAdditionOperators<T, Double, T>,
    ISubtractionOperators<T, Double, T>,
    IMultiplyOperators<T, Double, T>,
    IDivisionOperators<T, Double, T> => value * this.nominator / this.denominator + this.offset;
    public T Inverse<T>(in T value) where T :
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

    public static Polynomial Conversion<TFrom, TTo>()
        where TFrom : ITransform where TTo : ITransform => Cache<TFrom, TTo>.Polynomial;
    public static Double Convert<TFrom, TTo>(in Double value)
        where TFrom : ITransform where TTo : ITransform => Cache<TFrom, TTo>.Polynomial.Evaluate(in value);

    private static class Cache<T>
        where T : ITransform
    {
        public static readonly Polynomial Polynomial = Of(T.ToSi(new Transformation()));
    }

    private static class Cache<TFrom, TTo>
        where TFrom : ITransform
        where TTo : ITransform
    {
        public static readonly Polynomial Polynomial = Of(Of<TTo>().Inverse(TFrom.ToSi(new Transformation())));
    }
}