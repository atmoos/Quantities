namespace Quantities.Numerics;

public abstract class Polynomial
{
    public static Polynomial NoOp { get; } = new NoOpPoly();
    private Polynomial() { }
    public abstract Double Evaluate(Double value);
    public abstract Double Inverse(Double value);
    public static Polynomial Offset(in Double value) => new ShiftPoly(in value);
    public static Polynomial ScaleUp(in Double value) => new ScaleUpPoly(in value);
    public static Polynomial ScaleDown(in Double value) => new ScaleDownPoly(in value);
    public static Polynomial LinearUp(in Double scaling, in Double offset) => new LinearUpPoly(in scaling, in offset);
    public static Polynomial LinearDown(in Double scaling, in Double offset) => new LinearDownPoly(in scaling, in offset);
    public static Polynomial Fractional(in Double nominator, in Double denominator) => new FractionalPoly(in nominator, in denominator);
    public static Polynomial Full(in Double nominator, in Double denominator, in Double offset) => new FullPoly(in nominator, in denominator, in offset);

    private sealed class NoOpPoly : Polynomial
    {
        public override Double Evaluate(Double value) => value;
        public override Double Inverse(Double value) => value;
    }
    private sealed class ShiftPoly : Polynomial
    {
        private readonly Double offset;
        public ShiftPoly(in Double offset) => this.offset = offset;
        public override Double Evaluate(Double value) => value + this.offset;
        public override Double Inverse(Double value) => value - this.offset;
    }
    private sealed class ScaleUpPoly : Polynomial
    {
        private readonly Double scaling;
        public ScaleUpPoly(in Double scaling) => this.scaling = scaling;
        public override Double Evaluate(Double value) => this.scaling * value;
        public override Double Inverse(Double value) => value / this.scaling;
    }
    private sealed class ScaleDownPoly : Polynomial
    {
        private readonly Double scaling;
        public ScaleDownPoly(in Double scaling) => this.scaling = scaling;
        public override Double Evaluate(Double value) => value / this.scaling;
        public override Double Inverse(Double value) => this.scaling * value;
    }
    private sealed class LinearUpPoly : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearUpPoly(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Double Evaluate(Double value) => this.scaling * value + this.offset;
        public override Double Inverse(Double value) => (value - this.offset) / this.scaling;
    }
    private sealed class LinearDownPoly : Polynomial
    {
        private readonly Double scaling, offset;
        public LinearDownPoly(in Double scaling, in Double offset) => (this.scaling, this.offset) = (scaling, offset);
        public override Double Evaluate(Double value) => value / this.scaling + this.offset;
        public override Double Inverse(Double value) => this.scaling * (value - this.offset);
    }

    private sealed class FractionalPoly : Polynomial
    {
        private readonly Double nominator, denominator;
        public FractionalPoly(in Double nominator, in Double denominator) => (this.nominator, this.denominator) = (nominator, denominator);
        public override Double Evaluate(Double value) => this.nominator * value / this.denominator;
        public override Double Inverse(Double value) => this.denominator * value / this.nominator;
    }

    private sealed class FullPoly : Polynomial
    {
        private readonly Double nominator, denominator, offset;
        public FullPoly(in Double nominator, in Double denominator, in Double offset)
        {
            this.nominator = nominator;
            this.denominator = denominator;
            this.offset = offset;
        }
        public override Double Evaluate(Double value) => this.nominator * value / this.denominator + this.offset;
        public override Double Inverse(Double value) => this.denominator * (value - this.offset) / this.nominator;
    }
}
