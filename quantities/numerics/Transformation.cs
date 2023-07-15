namespace Quantities.Numerics;

public sealed class Transformation
{
    // These should not be declared as readonly!
    private Dekker nominator, denominator, offset;
    public Transformation()
    {
        this.offset = new Dekker(0d);
        this.nominator = this.denominator = new Dekker(1d);
    }

    private Transformation(in Dekker nominator, in Dekker denominator, in Dekker offset)
    {
        this.nominator = nominator;
        this.denominator = denominator;
        this.offset = offset;
    }

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

    internal Transformation Pow(Int32 exponent)
    {
        // This computes a "pseudo power". Just what we need in Quantities :-)
        /* ToDo:
         - use exponentiation by squaring;
         - enable negative exponents
         */
        const Int32 ownDegree = 1; // this instance already has degree 1;
        var (nominator, denominator) = (this.nominator, this.denominator);
        for (Int32 e = ownDegree; e < exponent; ++e) {
            this.nominator.Multiply(nominator);
            this.denominator.Multiply(denominator);
        }
        return this;
    }

    internal Transformation Invert()
    {
        var offset = -this.offset;
        offset.Divide(in this.nominator);
        offset.Multiply(in this.denominator);
        return new(in this.denominator, in this.nominator, in offset);
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
