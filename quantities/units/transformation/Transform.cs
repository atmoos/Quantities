namespace Quantities.Units.Transformation;

internal readonly struct Transform
{
    private readonly Double asSiUnit;
    internal Transform(Double asSiUnit)
    {
        this.asSiUnit = asSiUnit;
    }
    public Double ToSi(in Double nonSiValue) => nonSiValue * this.asSiUnit;
    public Double FromSi(in Double siValue) => siValue / this.asSiUnit;
}
