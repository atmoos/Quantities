namespace Quantities.Measures.Imperial;
internal static class BuildImperial<TImperial> where TImperial : IOther
{
    private static readonly Map map = new Map<OtherKernel<TImperial>>();
    public static Quant With(in Double value) => new(value, in map);
    public static Quant With(in Quant value)
    {
        var siValue = value.Project<OtherKernel<TImperial>>();
        return new(in siValue, in map);
    }
}