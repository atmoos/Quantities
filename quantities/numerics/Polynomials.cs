namespace Quantities.Numerics;

internal abstract class Polynomial
{
    public static Polynomial NoOp { get; } = new NoOpPoly();
    private Polynomial() { }
    public abstract Dekker Evaluate(in Dekker value);
    public abstract Dekker Inverse(in Dekker value);
    public static Polynomial Offset(in Dekker value) => new ShiftPoly(in value);
    public static Polynomial ScaleUp(in Dekker value) => new ScaleUpPoly(in value);
    public static Polynomial ScaleDown(in Dekker value) => new ScaleDownPoly(in value);
    public static Polynomial LinearUp(in Dekker scaling, in Dekker offset) => new LinearUpPoly(in scaling, in offset);
    public static Polynomial LinearDown(in Dekker scaling, in Dekker offset) => new LinearDownPoly(in scaling, in offset);
    public static Polynomial Fractional(in Dekker nominator, in Dekker denominator) => new FractionalPoly(in nominator, in denominator);
    public static Polynomial Full(in Dekker nominator, in Dekker denominator, in Dekker offset) => new FullPoly(in nominator, in denominator, in offset);

    private sealed class NoOpPoly : Polynomial
    {
        public override Dekker Evaluate(in Dekker value) => value;
        public override Dekker Inverse(in Dekker value) => value;
    }
    private sealed class ShiftPoly : Polynomial
    {
        private readonly Dekker offset;
        public ShiftPoly(in Dekker offset) => this.offset = offset;
        public override Dekker Evaluate(in Dekker value) => value + this.offset;
        public override Dekker Inverse(in Dekker value) => value - this.offset;
    }
    private sealed class ScaleUpPoly : Polynomial
    {
        private readonly Dekker scaling;
        public ScaleUpPoly(in Dekker scaling) => this.scaling = scaling;
        public override Dekker Evaluate(in Dekker value) => this.scaling * value;
        public override Dekker Inverse(in Dekker value) => value / this.scaling;
    }
    private sealed class ScaleDownPoly : Polynomial
    {
        private readonly Dekker scaling;
        public ScaleDownPoly(in Dekker scaling) => this.scaling = scaling;
        public override Dekker Evaluate(in Dekker value) => value / this.scaling;
        public override Dekker Inverse(in Dekker value) => this.scaling * value;
    }
    private sealed class LinearUpPoly : Polynomial
    {
        private readonly Dekker scaling, offset;
        public LinearUpPoly(in Dekker scaling, in Dekker offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Dekker Evaluate(in Dekker value) => this.scaling * value + this.offset;
        public override Dekker Inverse(in Dekker value) => (value - this.offset) / this.scaling;
    }
    private sealed class LinearDownPoly : Polynomial
    {
        private readonly Dekker scaling, offset;
        public LinearDownPoly(in Dekker scaling, in Dekker offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Dekker Evaluate(in Dekker value) => value / this.scaling + this.offset;
        public override Dekker Inverse(in Dekker value) => this.scaling * (value - this.offset);
    }

    private sealed class FractionalPoly : Polynomial
    {
        private readonly Dekker nominator, denominator;
        public FractionalPoly(in Dekker nominator, in Dekker denominator) => (this.nominator, this.denominator) = (nominator, denominator);
        public override Dekker Evaluate(in Dekker value) => this.nominator * value / this.denominator;
        public override Dekker Inverse(in Dekker value) => this.denominator * value / this.nominator;
    }

    private sealed class FullPoly : Polynomial
    {
        private readonly Dekker nominator, denominator, offset;
        public FullPoly(in Dekker nominator, in Dekker denominator, in Dekker offset)
        {
            this.nominator = nominator;
            this.denominator = denominator;
            this.offset = offset;
        }
        public override Dekker Evaluate(in Dekker value) => this.nominator * value / this.denominator + this.offset;
        public override Dekker Inverse(in Dekker value) => this.denominator * (value - this.offset) / this.nominator;
    }
}
