using System.Numerics;

namespace Quantities;

public sealed class Transformation :
    IAdditionOperators<Transformation, Double, Transformation>,
    ISubtractionOperators<Transformation, Double, Transformation>,
    IMultiplyOperators<Transformation, Double, Transformation>,
    IDivisionOperators<Transformation, Double, Transformation>
{
    private Double nominator, denominator, offset;
    internal Transformation()
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
        if (this.offset != 0) {
            var msg = $"{nameof(Pow)}({exponent}): Raising a {nameof(Transformation)} with a non zero offset ('{this.offset}') to any power is not yet implemented.";
            throw new NotImplementedException(msg);
        }
        this.nominator = Math.Pow(this.nominator, exponent);
        this.denominator = Math.Pow(this.denominator, exponent);
        return this;
    }
    internal Transformation Invert()
    {
        var offset = this.denominator * this.offset / this.nominator;
        return new(in this.denominator, in this.nominator, -offset);
    }
    internal void Deconstruct(out Double nominator, out Double denominator, out Double offset)
    {
        nominator = this.nominator;
        denominator = this.denominator;
        offset = this.offset;
    }

    public static Transformation operator +(Transformation left, Double right) => left.Add(in right);
    public static Transformation operator +(Double left, Transformation right) => right.Add(in left);
    public static Transformation operator -(Transformation left, Double right) => left.Subtract(in right);
    public static Transformation operator *(Transformation left, Double right) => left.Multiply(in right);
    public static Transformation operator *(Double left, Transformation right) => right.Multiply(in left);
    public static Transformation operator /(Transformation left, Double right) => left.Divide(in right);
}
