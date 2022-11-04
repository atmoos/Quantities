namespace Quantities.Unit.Transformation;

internal readonly struct Transform
{
    private readonly Double asSiUnit;
    private readonly Double fromSiUnit;
    internal Transform(Double asSiUnit)
    {
        this.asSiUnit = asSiUnit;
        this.fromSiUnit = 1d / asSiUnit;
    }
    public Double ToSi(in Double nonSiValue) => nonSiValue * this.asSiUnit;
    public Double FromSi(in Double siValue) => siValue * this.fromSiUnit;
}
