namespace Quantities.Measures.Other;

internal static class BuildOther<TOther> where TOther : IOther
{
    private static readonly Map map = new Map<OtherKernel<TOther>>();
    public static Quant With(in Double value) => new(value, in map);
    public static Quant With(in Quant value)
    {
        var siValue = value.Project<OtherKernel<TOther>>();
        return new(in siValue, in map);
    }
}