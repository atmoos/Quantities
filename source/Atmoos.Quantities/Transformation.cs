using System.Numerics;

namespace Atmoos.Quantities;

public sealed class Transformation :
    IAdditionOperators<Transformation, Double, Transformation>,
    ISubtractionOperators<Transformation, Double, Transformation>,
    IMultiplyOperators<Transformation, Double, Transformation>,
    IDivisionOperators<Transformation, Double, Transformation>
{
    private Double nominator, denominator, offset;
    internal Transformation() => (this.nominator, this.denominator, this.offset) = (1, 1, 0);
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

    internal void Deconstruct(out Double nominator, out Double denominator, out Double offset)
    {
        nominator = this.nominator;
        denominator = this.denominator;
        offset = this.offset;
    }

    public Transformation FusedMultiplyAdd(Double multiplicand, Double addend)
    {
        this.nominator *= multiplicand;
        this.offset = Double.FusedMultiplyAdd(multiplicand, this.offset, addend);
        return this;
    }

    public static Transformation operator +(Transformation left, Double right) => left.Add(in right);
    public static Transformation operator +(Double left, Transformation right) => right.Add(in left);
    public static Transformation operator -(Transformation left, Double right) => left.Subtract(in right);
    public static Transformation operator *(Transformation left, Double right) => left.Multiply(in right);
    public static Transformation operator *(Double left, Transformation right) => right.Multiply(in left);
    public static Transformation operator /(Transformation left, Double right) => left.Divide(in right);
}
