namespace Quantities.Numerics;

public sealed class Transformation
{
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
        this.offset += value;
        return this;
    }
    internal Transformation Subtract(in Double value)
    {
        this.offset -= value;
        return this;
    }

    internal Transformation Multiply(in Double value)
    {
        this.offset *= value;
        this.nominator *= value;
        return this;
    }

    internal Transformation Divide(in Double value)
    {
        this.offset /= value;
        this.denominator *= value;
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
            this.nominator *= nominator;
            this.denominator *= denominator;
        }
        return this;
    }

    internal Transformation Invert()
    {
        var offset = this.denominator * this.offset / this.nominator;
        return new(in this.denominator, in this.nominator, -offset);
    }

    internal Polynomial Build() => ((Double)this.nominator, (Double)this.denominator, (Double)this.offset) switch {
        (1, 1, 0) => Polynomial.NoOp,
        (1, 1, _) => Polynomial.Offset(this.offset),
        (_, 1, 0) => Polynomial.ScaleUp(this.nominator),
        (_, 1, _) => Polynomial.LinearUp(this.nominator, this.offset),
        (1, _, 0) => Polynomial.ScaleDown(this.denominator),
        (1, _, _) => Polynomial.LinearDown(this.denominator, this.offset),
        (_, _, 0) => Polynomial.Fractional(this.nominator, this.denominator),
        var (_, _, _) => Polynomial.Full(this.nominator, this.denominator, this.offset)
    };

    public static Transformation operator +(Transformation left, Double right) => left.Add(in right);
    public static Transformation operator +(Double left, Transformation right) => right.Add(in left);
    public static Transformation operator -(Transformation left, Double right) => left.Subtract(in right);
    public static Transformation operator *(Transformation left, Double right) => left.Multiply(in right);
    public static Transformation operator *(Double left, Transformation right) => right.Multiply(in left);
    public static Transformation operator /(Transformation left, Double right) => left.Divide(in right);
}
