namespace Quantities.Unit.Transformation;

internal readonly struct LinearTransform
{
    private readonly Decimal offset;
    private readonly Decimal nominator;
    private readonly Decimal denominator;
    internal LinearTransform(Decimal nominator, Decimal denominator, Decimal offset)
    {
        this.nominator = nominator;
        this.denominator = denominator;
        this.offset = offset;
    }
    public Double ToSi(in Double nonSiValue) => (Double)((this.nominator * (Decimal)nonSiValue / this.denominator) + this.offset);
    public Double FromSi(in Double siValue) => (Double)(this.denominator * ((Decimal)siValue - this.offset) / this.nominator);
}
