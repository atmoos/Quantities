namespace Quantities.Units.Transformation;

internal readonly struct LinearTransform
{
    private readonly Decimal offset;
    private readonly Decimal nominator;
    private readonly Decimal denominator;
    internal LinearTransform(Decimal nominator, Decimal denominator, Decimal offset = 0)
    {
        this.nominator = nominator;
        this.denominator = denominator;
        this.offset = offset;
    }
    public Double ToSi(in Double self) => (Double)((this.nominator * (Decimal)self / this.denominator) + this.offset);
    public Double FromSi(in Double value) => (Double)(this.denominator * ((Decimal)value - this.offset) / this.nominator);
}
