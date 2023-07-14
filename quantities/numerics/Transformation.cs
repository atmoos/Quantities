namespace Quantities.Numerics;

public sealed class Transformation
{
    private Dekker nominator = new(1d);
    private Dekker denominator = new(1d);
    private Dekker offset = new(0d);

    internal Transformation Add(in Double value)
    {
        this.offset.Add(in value);
        return this;
    }
    internal Transformation Subtract(in Double value)
    {
        this.offset.Subtract(in value);
        return this;
    }

    internal Transformation Multiply(in Double value)
    {
        this.offset.Multiply(in value);
        this.nominator.Multiply(in value);
        return this;
    }
    internal Transformation Divide(in Double value)
    {
        this.offset.Divide(in value);
        this.denominator.Multiply(in value);
        return this;
    }

    internal Polynomial Build() => (this.nominator.Value, this.denominator.Value, this.offset.Value) switch {
        (1, 1, 0) => Polynomial.NoOp,
        (1, 1, var o) => Polynomial.Offset(o),
        (var s, 1, 0) => Polynomial.ScaleUp(s),
        (var s, 1, var o) => Polynomial.LinearUp(s, o),
        (1, var d, 0) => Polynomial.ScaleDown(d),
        (1, var d, var o) => Polynomial.LinearDown(d, o),
        (var n, var d, 0) => Polynomial.Fractional(n, d),
        var (n, d, o) => Polynomial.Full(n, d, o)
    };

    public static Transformation operator +(Transformation left, Double right) => left.Add(in right);
    public static Transformation operator +(Double left, Transformation right) => right.Add(in left);
    public static Transformation operator -(Transformation left, Double right) => left.Subtract(in right);
    public static Transformation operator *(Transformation left, Double right) => left.Multiply(in right);
    public static Transformation operator *(Double left, Transformation right) => right.Multiply(in left);
    public static Transformation operator /(Transformation left, Double right) => left.Divide(in right);
}
