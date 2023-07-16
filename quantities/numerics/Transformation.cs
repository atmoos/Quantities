namespace Quantities.Numerics;

public sealed class Transformation
{
    private Double nominator, denominator, offset;
    public Transformation()
    {
        this.offset = 0d;
        this.nominator = this.denominator = 1d;
    }
    private Transformation(in Double nominator, in Double denominator, in Double offset)
    {
        this.nominator = nominator;
        this.denominator = denominator;
        this.offset = offset;
    }
    private Transformation Add(in Double value)
    {
        this.offset += value;
        return this;
    }
    private Transformation Subtract(in Double value)
    {
        this.offset -= value;
        return this;
    }
    private Transformation Multiply(in Double value)
    {
        this.offset *= value;
        this.nominator *= value;
        return this;
    }
    private Transformation Divide(in Double value)
    {
        this.offset /= value;
        this.denominator *= value;
        return this;
    }
    internal Transformation Pow(Int32 exponent)
    {
        this.nominator = Math.Pow(this.nominator, exponent);
        this.denominator = Math.Pow(this.denominator, exponent);
        return this;
    }
    internal Transformation Invert()
    {
        var offset = this.denominator * this.offset / this.nominator;
        return new(in this.denominator, in this.nominator, -offset);
    }
    internal Polynomial Build() => (this.nominator, this.denominator, this.offset) switch {
        (1, 1, 0) => Polynomial.NoOp,
        (1, 1, var o) => Polynomial.Offset(in o),
        (var s, 1, 0) => Polynomial.ScaleUp(in s),
        (var s, 1, var o) => Polynomial.LinearUp(in s, in o),
        (1, var d, 0) => Polynomial.ScaleDown(in d),
        (1, var d, var o) => Polynomial.LinearDown(in d, in o),
        (var n, var d, 0) => Polynomial.Fractional(in n, in d),
        var (n, d, o) => Polynomial.Full(in n, in d, in o)
    };

    public static Transformation operator +(Transformation left, Double right) => left.Add(in right);
    public static Transformation operator +(Double left, Transformation right) => right.Add(in left);
    public static Transformation operator -(Transformation left, Double right) => left.Subtract(in right);
    public static Transformation operator *(Transformation left, Double right) => left.Multiply(in right);
    public static Transformation operator *(Double left, Transformation right) => right.Multiply(in left);
    public static Transformation operator /(Transformation left, Double right) => left.Divide(in right);
}
