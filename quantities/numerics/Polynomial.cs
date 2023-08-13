namespace Quantities.Numerics;

internal abstract class Polynomial
{
    public static Polynomial NoOp { get; } = new NoOpPoly();
    private Polynomial() { }
    public abstract Double Evaluate(in Double value);
    public abstract Double Inverse(in Double value);
    public static Polynomial Of(Transformation transformation)
    {
        var (nominator, denominator, offset) = transformation;
        return Of(in nominator, in denominator, in offset);
    }
    public static Polynomial Of<TTransform>()
        where TTransform : ITransform
    {
        return Of(TTransform.ToSi(new Transformation()));
    }
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
        public override Double Evaluate(in Double value) => value;
        public override Double Inverse(in Double value) => value;
    }
    private sealed class Shift : Polynomial
    {
        private readonly Double offset;
        public Shift(in Double offset) => this.offset = offset;
        public override Double Evaluate(in Double value) => value + this.offset;
        public override Double Inverse(in Double value) => value - this.offset;
    }
    private sealed class ScaleUp : Polynomial
    {
        private readonly Double scaling;
        public ScaleUp(in Double scaling) => this.scaling = scaling;
        public override Double Evaluate(in Double value) => this.scaling * value;
        public override Double Inverse(in Double value) => value / this.scaling;
    }
    private sealed class ScaleDown : Polynomial
    {
        private readonly Double scaling;
        public ScaleDown(in Double scaling) => this.scaling = scaling;
        public override Double Evaluate(in Double value) => value / this.scaling;
        public override Double Inverse(in Double value) => this.scaling * value;
    }
    private sealed class LinearUp : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearUp(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Double Evaluate(in Double value) => this.scaling * value + this.offset;
        public override Double Inverse(in Double value) => (value - this.offset) / this.scaling;
    }
    private sealed class LinearDown : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearDown(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Double Evaluate(in Double value) => value / this.scaling + this.offset;
        public override Double Inverse(in Double value) => this.scaling * (value - this.offset);
    }

    private sealed class Fraction : Polynomial
    {
        private readonly Double nominator, denominator;
        public Fraction(in Double nominator, in Double denominator) => (this.nominator, this.denominator) = (nominator, denominator);
        public override Double Evaluate(in Double value) => this.nominator * value / this.denominator;
        public override Double Inverse(in Double value) => this.denominator * value / this.nominator;
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
        public override Double Evaluate(in Double value) => this.nominator * value / this.denominator + this.offset;
        public override Double Inverse(in Double value) => this.denominator * (value - this.offset) / this.nominator;
    }
}
