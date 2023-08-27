using System.Numerics;

namespace Quantities.Numerics;

file static class Cache<TTransform>
    where TTransform : ITransform
{
    public static Polynomial Poly { get; } = Polynomial.Of(TTransform.ToSi(new Transformation()));
}

internal abstract class Polynomial
{
    public static Polynomial NoOp { get; } = new NoOpPoly();
    private Polynomial() { }
    public abstract T Evaluate<T>(in T value) where T :
    IAdditionOperators<T, Double, T>,
    ISubtractionOperators<T, Double, T>,
    IMultiplyOperators<T, Double, T>,
    IDivisionOperators<T, Double, T>;
    public abstract T Inverse<T>(in T value) where T :
    IAdditionOperators<T, Double, T>,
    ISubtractionOperators<T, Double, T>,
    IMultiplyOperators<T, Double, T>,
    IDivisionOperators<T, Double, T>;
    public static Polynomial Of(Transformation transformation)
    {
        var (nominator, denominator, offset) = transformation;
        return Of(in nominator, in denominator, in offset);
    }
    public static Polynomial Of<TTransform>()
        where TTransform : ITransform => Cache<TTransform>.Poly;
    private static Polynomial Of(in Double nominator, in Double denominator, in Double offset) => (nominator, denominator, offset) switch {
        (1, 1, 0) => NoOp,
        (1, 1, var o) => new Shift(in o),
        (var s, 1, 0) => new ScaleUp(in s),
        (var s, 1, var o) => new LinearUp(in s, in o),
        (1, var d, 0) => new ScaleDown(in d),
        (1, var d, var o) => new LinearDown(in d, in o),
        (var n, var d, 0) => new Fraction(in n, in d),
        var (n, d, o) => new Full(in n, in d, in o)
    };

    private sealed class NoOpPoly : Polynomial
    {
        public override T Evaluate<T>(in T value) => value;
        public override T Inverse<T>(in T value) => value;
    }
    private sealed class Shift : Polynomial
    {
        private readonly Double offset;
        public Shift(in Double offset) => this.offset = offset;
        public override T Evaluate<T>(in T value) => value + this.offset;
        public override T Inverse<T>(in T value) => value - this.offset;
    }
    private sealed class ScaleUp : Polynomial
    {
        private readonly Double upscale;
        public ScaleUp(in Double scaling) => this.upscale = scaling;
        public override T Evaluate<T>(in T value) => value * this.upscale;
        public override T Inverse<T>(in T value) => value / this.upscale;
    }
    private sealed class ScaleDown : Polynomial
    {
        private readonly Double downscale;
        public ScaleDown(in Double scaling) => this.downscale = scaling;
        public override T Evaluate<T>(in T value) => value / this.downscale;
        public override T Inverse<T>(in T value) => value * this.downscale;
    }
    private sealed class LinearUp : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearUp(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override T Evaluate<T>(in T value) => (value * this.scaling) + this.offset;
        public override T Inverse<T>(in T value) => (value - this.offset) / this.scaling;
    }
    private sealed class LinearDown : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearDown(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override T Evaluate<T>(in T value) => (value / this.scaling) + this.offset;
        public override T Inverse<T>(in T value) => (value - this.offset) * this.scaling;
    }

    private sealed class Fraction : Polynomial
    {
        private readonly Double nominator, denominator;
        public Fraction(in Double nominator, in Double denominator) => (this.nominator, this.denominator) = (nominator, denominator);
        public override T Evaluate<T>(in T value) => value * this.nominator / this.denominator;
        public override T Inverse<T>(in T value) => value * this.denominator / this.nominator;
    }

    private sealed class Full : Polynomial
    {
        private readonly Double nominator, denominator, offset;
        public Full(in Double nominator, in Double denominator, in Double offset)
        {
            this.nominator = nominator;
            this.denominator = denominator;
            this.offset = offset;
        }
        public override T Evaluate<T>(in T value) => value * this.nominator / this.denominator + this.offset;
        public override T Inverse<T>(in T value) => (value - this.offset) * this.denominator / this.nominator;
    }
}
